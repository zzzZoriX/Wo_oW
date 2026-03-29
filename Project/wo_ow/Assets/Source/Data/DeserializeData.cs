using System.IO;
using UnityEngine;

public static class DeserializeData
{
    public static T Deserialize<T>(string fileName) {
        var jsonText = Resources.Load<TextAsset>(fileName);
        return JsonUtility.FromJson<T>(jsonText.text);
    }
}