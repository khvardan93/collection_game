using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

namespace Core.Services
{
    public sealed class DataStorage : IDataStorage
    {
        private readonly string _filePath;
        private readonly Dictionary<string, string> _entries;

        public DataStorage() : this(Path.Combine(Application.persistentDataPath, "save.json"))
        {
        }

        public DataStorage(string filePath)
        {
            _filePath = filePath;
            _entries = LoadEntries(_filePath);
        }

        void IDataStorage.Save<T>(T data)
        {
            if (!Attribute.IsDefined(typeof(T), typeof(SerializableAttribute)))
            {
                Debug.LogError($"{typeof(T).Name} must have the [Serializable] attribute.");
                return;
            }
            
            _entries[data.Key] = JsonUtility.ToJson(data);
            Flush();
        }

        T IDataStorage.Load<T>(string key)
        {
            if (!_entries.TryGetValue(key, out string json))
            {
                return new T();
            }

            return JsonUtility.FromJson<T>(json);
        }

        bool IDataStorage.Exists(string key)
        {
            return _entries.ContainsKey(key);
        }

        void IDataStorage.Delete(string key)
        {
            if (_entries.Remove(key))
            {
                Flush();
            }
        }

        private void Flush()
        {
            var file = new SaveFile();

            foreach (KeyValuePair<string, string> entry in _entries)
            {
                file.entries.Add(new Entry { key = entry.Key, json = entry.Value });
            }

            File.WriteAllText(_filePath, JsonUtility.ToJson(file));
        }

        private static Dictionary<string, string> LoadEntries(string filePath)
        {
            var result = new Dictionary<string, string>();

            if (!File.Exists(filePath))
            {
                return result;
            }

            string json = File.ReadAllText(filePath);
            var file = JsonUtility.FromJson<SaveFile>(json);

            if (file?.entries == null)
            {
                return result;
            }

            foreach (Entry entry in file.entries)
            {
                result[entry.key] = entry.json;
            }

            return result;
        }

        [Serializable]
        private sealed class Entry
        {
            public string key;
            public string json;
        }

        [Serializable]
        private sealed class SaveFile
        {
            public List<Entry> entries = new List<Entry>();
        }
    }
}
