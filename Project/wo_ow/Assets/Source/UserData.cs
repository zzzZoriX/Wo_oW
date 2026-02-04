using System.IO;
using UnityEngine;

[System.Serializable]
public class UserData
{
    public float sensitivity;

    public static UserData Deserialize(string path)
    {
        var jsonUserData = File.ReadAllText(path);
        return JsonUtility.FromJson<UserData>(jsonUserData);
    }
}