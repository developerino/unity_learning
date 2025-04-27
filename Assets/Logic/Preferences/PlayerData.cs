using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private PlayerPreferences playerPreferences;

    public static PlayerData Instance { get; private set; }
    public static PlayerPreferences PlayerPreferences => Instance.playerPreferences;

    private void Awake()
    {
        Instance = this;
        playerPreferences = new PlayerPreferences();
    }
}