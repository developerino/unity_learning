namespace Test
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using UnityEngine;


    public class PlayerPreferences
    {
        private Dictionary<string, object> preferences = new Dictionary<string, object>();

        public void LoadPrefs()
        {
            if (!HasPref("preferences"))
            {
                return;
            }
            string LocalData = GetPref<string>("preferences");
            this.preferences = JsonConvert.DeserializeObject<Dictionary<string, object>>(LocalData);
        }


        private void SavePrefs()
        {
            PlayerPrefs.SetString("preferences", JsonConvert.SerializeObject(this.preferences));
            PlayerPrefs.Save();
        }

        public void SetPref<T>(string key, T value, bool savePrefs = true)
        {
            this.preferences[key] = value;

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
                default:
                    if (typeof(T).IsEnum)
                    {
                        PlayerPrefs.SetInt(key, (int)(object)value);
                    }
                    else
                    {
                        throw new ArgumentException($"Unsupported type: {typeof(T)}");
                    }
                    break;
            }

            if (savePrefs)
            {
                SavePrefs();
            }
        }

        public T GetPref<T>(string key)
        {
            if (preferences.TryGetValue(key, out object value))
            {
                if (typeof(T).IsEnum)
                {
                    return (T)Enum.ToObject(typeof(T), Convert.ToInt32(value));
                }
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

            throw new ArgumentException($"Unsupported type: {typeof(T)}");
        }

        public T GetPref<T>(string key, T defaultValue)
        {
            if (!this.HasPref(key))
            {
                return defaultValue;
            }


            return this.GetPref<T>(key);
        }


        public bool HasPref(string key)
        {
            return this.preferences.ContainsKey(key) || PlayerPrefs.HasKey(key);
        }
    }
}
