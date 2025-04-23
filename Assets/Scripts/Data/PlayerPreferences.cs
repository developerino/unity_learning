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


            if (typeof(T) == typeof(string))
            {
                PlayerPrefs.SetString(key, (string)(object)value);
            }
            else if (typeof(T) == typeof(bool))
            {
                PlayerPrefs.SetInt(key, ((bool)(object)value ? 1 : 0));
            }
            else if (value is Enum)
            {
                PlayerPrefs.SetInt(key, (int)(object)value);
            }
            else
            {
                throw new ArgumentException();
            }


            if (savePrefs)
            {
                this.SavePrefs();
            }
        }


        public T GetPref<T>(string key)
        {
            if (this.preferences.TryGetValue(key, out object value))
            {
                if (typeof(Enum).IsAssignableFrom(typeof(T)))
                {
                    return (T)(object)Convert.ToInt32(value);
                }


                return (T)this.preferences[key];
            }


            if (typeof(T) == typeof(string))
            {
                return (T)(object)PlayerPrefs.GetString(key);
            }


            if (typeof(T) == typeof(bool))
            {
                return (T)(object)(PlayerPrefs.GetInt(key) != 0);
            }


            if (typeof(Enum).IsAssignableFrom(typeof(T)))
            {
                return (T)(object)Convert.ToInt32(PlayerPrefs.GetInt(key));
            }


            throw new ArgumentException();
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
