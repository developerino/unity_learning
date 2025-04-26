using UnityEngine;

public class AutoSingleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            if (_instance == null)
            {
                // Check if the prefab is in the Resources folder and load it if not found
                _instance = FindObjectOfType<T>();

                if (_instance == null)
                {
                    // Load the prefab from the Resources folder and instantiate it
                    GameObject prefab = Resources.Load<GameObject>("Prefabs/GameManagerPrefab");
                    if (prefab != null)
                    {
                        _instance = Instantiate(prefab).GetComponent<T>();
                    }
                    else
                    {
                        Debug.LogError("GameManager prefab not found in Resources.");
                    }
                }
            }

            return _instance;
        }
    }
}
