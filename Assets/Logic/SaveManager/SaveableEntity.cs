using System;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[DisallowMultipleComponent]
public class SaveableEntity : MonoBehaviour
{
    [Header("Saveable Entity Settings")]
    [SerializeField] private string stableID = string.Empty;
    [SerializeField] private string prefabID = string.Empty;
    [SerializeField] private SaveFlags saveFlags = SaveFlags.Position | SaveFlags.Rotation;
    public string StableID => stableID;
    public string PrefabID => prefabID;
    public SaveFlags Flags => saveFlags;

    private void Reset()
    {
        GenerateID();
    }

    private void Awake()
    {
        if (string.IsNullOrEmpty(stableID))
            GenerateID();

        if (string.IsNullOrEmpty(prefabID))
            prefabID = gameObject.name.Replace("(Clone)", "").Trim();
    }


    private void GenerateID()
    {
        stableID = Guid.NewGuid().ToString();
    }

#if UNITY_EDITOR
    private void OnValidate()
    {
        if (string.IsNullOrEmpty(stableID))
        {
            UnityEditor.EditorApplication.delayCall += () =>
            {
                if (this != null && string.IsNullOrEmpty(stableID))
                    GenerateID();
            };
        }
    }
#endif
}
