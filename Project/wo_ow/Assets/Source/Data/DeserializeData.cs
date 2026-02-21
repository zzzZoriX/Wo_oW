using System.IO;
using UnityEngine;

public class DeserializeData
{
    public static T Deserialize<T>(string path)
    {
        var jsonUserData = File.ReadAllText(path);
        return JsonUtility.FromJson<T>(jsonUserData);
    }
}