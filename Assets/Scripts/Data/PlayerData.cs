using Test;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    private PlayerPreferences playerPreferences;
    public static PlayerData Instance { get; private set; }
    public static PlayerPreferences PlayerPreferences => Instance.playerPreferences;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
