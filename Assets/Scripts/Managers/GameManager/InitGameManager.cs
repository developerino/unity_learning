using UnityEngine;

public class InitGameManager
{
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    static void InitializeGameManager()
    {
        GameObject gameManagerPrefab = Resources.Load<GameObject>("Prefabs/GameManagerPrefab");

        if (gameManagerPrefab != null)
        {
            Debug.Log("Instantiate the GameManager prefab and set it to not be destroyed on load.");
            GameObject gameManagerInstance = Object.Instantiate(gameManagerPrefab);
            Object.DontDestroyOnLoad(gameManagerInstance);
        }
        else
        {
            Debug.LogError("GameManagerPrefab could not be found in Resources/Prefabs.");
        }
    }
}
