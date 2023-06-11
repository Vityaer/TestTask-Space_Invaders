using Json;
using Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace Utils
{
    public static class TextUtils
    {
        public static string GetTextFromLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            if (!File.Exists(path))
            {
                File.Create(path).Close();
            }
            var text = File.ReadAllText(path);
            return text;
        }

        public static string GetConfigPath<T>()
        {
            var path = Path.Combine(Constants.Common.DictionariesPath, $"{typeof(T).Name}.json");
            return path;
        }

        public static void Save<T>(List<T> data)
        {
            var json = JsonConvert.SerializeObject(data, Constants.Common.SerializerSettings);
            File.WriteAllText(GetConfigPath<T>(), json);
        }


        public static void Save<T>(T data)
        {
            File.WriteAllText(GetConfigPath<T>(),
                JsonConvert.SerializeObject(data, Constants.Common.SerializerSettings));
        }

        public static bool IsLoadedToLocalStorage<T>()
        {
            var path = GetConfigPath<T>();
            return File.Exists(path);
        }

        public static Dictionary<string, T> FillDictionary<T>(string jsonData, IJsonConverter converter)
            where T : BaseModel
        {
            var fromJson = converter.FromJson<List<T>>(jsonData);
            var result = new Dictionary<string, T>();

            if(string.IsNullOrEmpty(jsonData))
                return result;

            foreach (var unit in fromJson)
            {
                result.Add(unit.Id, unit);
            }

            return result;
        }

        public static T FillModel<T>(string jsonData, IJsonConverter jsonConverter)
        {
            var data = JsonConvert.DeserializeObject<T>(jsonData, Constants.Common.SerializerSettings);
            return data;
        }

        public static List<T> Save<T>(string jsonData, IJsonConverter converter) where T : BaseModel
        {
            var fromJson = converter.FromJson<List<T>>(jsonData);
            Save(fromJson);
            return fromJson;
        }

        public static void Save<T>(string jsonData)
        {
            File.WriteAllText(GetConfigPath<T>(), jsonData);
        }

        public static StreamWriter GetFileWriterStream(string path, string fileName, bool append)
        {
            var filePath = Path.Combine(path, fileName);

            if (!File.Exists(filePath))
            {
                if (!File.Exists(path))
                    Directory.CreateDirectory(path);
            }

            return new StreamWriter(filePath, append: append);
        }
    }
}