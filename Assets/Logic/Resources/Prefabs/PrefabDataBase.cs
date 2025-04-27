using System.Collections.Generic;
using UnityEngine;

public static class PrefabDatabase
{
    private static Dictionary<string, GameObject> prefabLookup = new Dictionary<string, GameObject>();
    private static bool initialized = false;

    public static void Initialize()
    {
        if (initialized) return;

        GameObject[] prefabs = Resources.LoadAll<GameObject>("Prefabs"); // Folder: Assets/Resources/Prefabs/

        foreach (GameObject prefab in prefabs)
        {
            if (prefabLookup.ContainsKey(prefab.name))
            {
                Debug.LogWarning($"PrefabDatabase: Duplicate prefab name found: {prefab.name}");
                continue;
            }

            prefabLookup.Add(prefab.name, prefab);
        }

        initialized = true;
        Debug.Log($"PrefabDatabase: Loaded {prefabLookup.Count} prefabs.");
    }

    public static GameObject GetPrefab(string prefabID)
    {
        if (!initialized)
            Initialize();

        if (prefabLookup.TryGetValue(prefabID, out GameObject prefab))
        {
            return prefab;
        }

        Debug.LogError($"PrefabDatabase: Prefab with ID '{prefabID}' not found!");
        return null;
    }
}
