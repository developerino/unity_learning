using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class SaveManager : MonoBehaviour
{
    private static string SaveFolder => Application.persistentDataPath;
    private static string SaveFilePath => Path.Combine(SaveFolder, "savefile.json");

    public static void SaveGame()
    {
        Debug.Log("SaveManager: Saving game...");

        WorldSaveData worldData = new WorldSaveData();
        worldData.CurrentSceneName = UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        SaveableEntity[] entities = GameObject.FindObjectsOfType<SaveableEntity>();

        foreach (var entity in entities)
        {
            EntitySaveData data = new EntitySaveData
            {
                StableID = entity.StableID,
                PrefabID = entity.PrefabID,
                Position = SerializationUtils.SerializeVector3(entity.transform.position),
                Rotation = SerializationUtils.SerializeVector3(entity.transform.rotation.eulerAngles),
                Scale = SerializationUtils.SerializeVector3(entity.transform.localScale),
                ExtraData = new Dictionary<string, string>() // Can be populated later
            };

            worldData.Entities.Add(data);
        }

        string json = JsonUtility.ToJson(worldData, prettyPrint: true);
        File.WriteAllText(SaveFilePath, json);

        Debug.Log("SaveManager: Game saved to " + SaveFilePath);
    }

    public static void LoadGame()
    {
        Debug.Log("SaveManager: Loading game...");

        if (!File.Exists(SaveFilePath))
        {
            Debug.LogWarning("SaveManager: No save file found at " + SaveFilePath);
            return;
        }

        string json = File.ReadAllText(SaveFilePath);
        WorldSaveData worldData = JsonUtility.FromJson<WorldSaveData>(json);

        if (worldData == null)
        {
            Debug.LogError("SaveManager: Failed to parse save data.");
            return;
        }

        SaveableEntity[] existingEntities = GameObject.FindObjectsOfType<SaveableEntity>();

        foreach (var entityData in worldData.Entities)
        {
            SaveableEntity foundEntity = null;

            // Try to find if entity already exists
            foreach (var entity in existingEntities)
            {
                if (entity.StableID == entityData.StableID)
                {
                    foundEntity = entity;
                    break;
                }
            }

            if (foundEntity != null)
            {
                // Update existing entity
                Debug.Log($"SaveManager: Found existing entity {foundEntity.name}, updating...");
                foundEntity.transform.position = SerializationUtils.DeserializeVector3(entityData.Position);
                foundEntity.transform.rotation = Quaternion.Euler(SerializationUtils.DeserializeVector3(entityData.Rotation));
                foundEntity.transform.localScale = SerializationUtils.DeserializeVector3(entityData.Scale);
            }
            else
            {
                // Spawn new entity
                GameObject prefab = PrefabDatabase.GetPrefab(entityData.PrefabID);
                if (prefab == null)
                {
                    Debug.LogError("SaveManager: Could not find prefab for ID: " + entityData.PrefabID);
                    continue;
                }

                GameObject instance = Object.Instantiate(prefab);
                instance.transform.position = SerializationUtils.DeserializeVector3(entityData.Position);
                instance.transform.rotation = Quaternion.Euler(SerializationUtils.DeserializeVector3(entityData.Rotation));
                instance.transform.localScale = SerializationUtils.DeserializeVector3(entityData.Scale);

                SaveableEntity saveable = instance.GetComponent<SaveableEntity>();
                if (saveable != null)
                {
                    typeof(SaveableEntity)
                        .GetField("stableID", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                        ?.SetValue(saveable, entityData.StableID);
                }
            }
        }

        Debug.Log("SaveManager: Game loaded successfully.");
    }
}
