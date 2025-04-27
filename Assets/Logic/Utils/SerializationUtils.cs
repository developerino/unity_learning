using UnityEngine;

public static class SerializationUtils
{
    // Serialize a Vector3 to a format suitable for saving
    public static string SerializeVector3(Vector3 position)
    {
        Vector3Data serializedPosition = new Vector3Data(position);
        return JsonUtility.ToJson(serializedPosition);
    }

    // Deserialize a string back into a Vector3
    public static Vector3 DeserializeVector3(string serializedData)
    {
        Vector3Data serializedPosition = JsonUtility.FromJson<Vector3Data>(serializedData);
        return serializedPosition.ToVector3();
    }

    [System.Serializable]
    public class Vector3Data
    {
        public float x, y, z;

        public Vector3Data(Vector3 position)
        {
            x = position.x;
            y = position.y;
            z = position.z;
        }

        public Vector3 ToVector3() => new Vector3(x, y, z);
    }
}
