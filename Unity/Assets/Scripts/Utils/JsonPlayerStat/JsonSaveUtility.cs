using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class JsonSaveUtility
{
    public static void SaveDictionary(string jsonKey, Dictionary<string, int> dictionary)
    {
        SerializableDictionary serializableDict = new SerializableDictionary();

        foreach (var pair in dictionary)
        {
            serializableDict.keys.Add(pair.Key);
            serializableDict.values.Add(pair.Value);
        }

        string jsonData = JsonUtility.ToJson(serializableDict, true);
        string path = Application.persistentDataPath + "/" + jsonKey + ".json";
        File.WriteAllText(path, jsonData);
    }

    public static Dictionary<string, int> LoadDictionary(string jsonKey)
    {
        Dictionary<string, int> dictionary = new Dictionary<string, int>();
        string path = Application.persistentDataPath + "/" + jsonKey + ".json";

        if (File.Exists(path))
        {
            string jsonData = File.ReadAllText(path);
            SerializableDictionary serializableDict = JsonUtility.FromJson<SerializableDictionary>(jsonData);

            for (int i = 0; i < serializableDict.keys.Count; i++)
            {
                dictionary.Add(serializableDict.keys[i], serializableDict.values[i]);
            }
        }

        return dictionary;
    }
}

[System.Serializable]
public class SerializableDictionary
{
    public List<string> keys = new List<string>();
    public List<int> values = new List<int>(); 
}