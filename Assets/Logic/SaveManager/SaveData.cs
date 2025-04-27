using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class WorldSaveData
{
    public int SaveVersion = 1;
    public string CurrentSceneName = "";
    public List<EntitySaveData> Entities = new List<EntitySaveData>();
}

[Serializable]
public class EntitySaveData
{
    public string StableID;
    public string PrefabID;
    public string Position;   // serialized Vector3 as string
    public string Rotation;   // serialized Vector3 as string
    public string Scale;      // serialized Vector3 as string
    public Dictionary<string, string> ExtraData = new Dictionary<string, string>();
}
