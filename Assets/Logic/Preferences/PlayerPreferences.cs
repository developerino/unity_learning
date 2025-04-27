using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class PlayerPreferences
{
    private Dictionary<string, object> preferences = new Dictionary<string, object>();
    private bool autoSaveOnChange;

    public PlayerPreferences(bool autoSaveOnChange = true)
    {
        this.autoSaveOnChange = autoSaveOnChange;
    }

    public void LoadPrefs()
    {
        if (!HasPref("preferences"))
            return;

        string data = PlayerPrefs.GetString("preferences");
        preferences = JsonConvert.DeserializeObject<Dictionary<string, object>>(data) ?? new Dictionary<string, object>();
    }

    private void SavePrefs()
    {
        PlayerPrefs.SetString("preferences", JsonConvert.SerializeObject(preferences));
        PlayerPrefs.Save();
    }

    public void SetPref<T>(string key, T value)
    {
        if (typeof(T) == typeof(Vector3))
        {
            // If it's a Vector3, flatten it first
            var serialized = SerializationUtils.SerializeVector3((Vector3)(object)value);
            preferences[key] = serialized; // Save the flattened json string into memory
            PlayerPrefs.SetString(key, serialized);
        }
        else
        {
            preferences[key] = value;

            switch (value)
            {
                case string s:
                    PlayerPrefs.SetString(key, s);
                    break;
                case bool b:
                    PlayerPrefs.SetInt(key, b ? 1 : 0);
                    break;
                case int i:
                    PlayerPrefs.SetInt(key, i);
                    break;
                case float f:
                    PlayerPrefs.SetFloat(key, f);
                    break;
                case double d:
                    PlayerPrefs.SetString(key, d.ToString());
                    break;
                case Enum e:
                    PlayerPrefs.SetInt(key, Convert.ToInt32(e));
                    break;
                default:
                    throw new ArgumentException($"Unsupported type: {typeof(T)}");
            }
        }

        if (autoSaveOnChange)
            SavePrefs();
    }

    public T GetPref<T>(string key, T defaultValue = default)
    {
        if (!HasPref(key))
            return defaultValue;

        if (preferences.TryGetValue(key, out object value))
        {
            if (typeof(T) == typeof(Vector3))
            {
                string serialized = value as string;
                if (!string.IsNullOrEmpty(serialized))
                    return (T)(object)SerializationUtils.DeserializeVector3(serialized);
                else
                    return (T)(object)Vector3.zero;
            }

            if (typeof(T).IsEnum)
                return (T)Enum.ToObject(typeof(T), Convert.ToInt32(value));

            return (T)Convert.ChangeType(value, typeof(T));
        }

        if (typeof(T) == typeof(string))
            return (T)(object)PlayerPrefs.GetString(key);
        if (typeof(T) == typeof(bool))
            return (T)(object)(PlayerPrefs.GetInt(key) != 0);
        if (typeof(T) == typeof(int))
            return (T)(object)PlayerPrefs.GetInt(key);
        if (typeof(T) == typeof(float))
            return (T)(object)PlayerPrefs.GetFloat(key);
        if (typeof(T) == typeof(double))
            return (T)(object)double.Parse(PlayerPrefs.GetString(key));
        if (typeof(T).IsEnum)
            return (T)Enum.ToObject(typeof(T), PlayerPrefs.GetInt(key));
        if (typeof(T) == typeof(Vector3))
        {
            string serialized = PlayerPrefs.GetString(key);
            return (T)(object)SerializationUtils.DeserializeVector3(serialized);
        }

        throw new ArgumentException($"Unsupported type: {typeof(T)}");
    }

    public bool HasPref(string key)
    {
        return preferences.ContainsKey(key) || PlayerPrefs.HasKey(key);
    }
}
