using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using UnityEditor;
using UnityEngine;

namespace Utils
{
    public static class EditorUtils
    {
        public static List<T> LoadList<T>()
        {
            var text = GetTextFromLocalStorage<T>();
            var data = JsonConvert.DeserializeObject<List<T>>(text, Constants.Common.SerializerSettings);
            if (data == null || data.Count == 0)
            {
                data = new List<T>();
            }

            return data;
        }

        public static T Load<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            var text = File.ReadAllText(path);
            var data = JsonConvert.DeserializeObject<T>(text, Constants.Common.SerializerSettings);
            return data;
        }

        public static void Save<T>(List<T> data)
        {
            File.WriteAllText(GetConfigPath<T>(),
                JsonConvert.SerializeObject(data, Constants.Common.SerializerSettings));
        }

        public static void Save<T>(T data)
        {
            File.WriteAllText(GetConfigPath<T>(),
                JsonConvert.SerializeObject(data, Constants.Common.SerializerSettings));
        }

        public static string GetConfigPath<T>()
        {
            var path = Path.Combine(Constants.Common.DictionariesPath, $"{typeof(T).Name}.json");
            return path;
        }

        private static string GetTextFromLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
            {
                File.Create(path);
            }

            var text = File.ReadAllText(path);
            return text;
        }
    }
}