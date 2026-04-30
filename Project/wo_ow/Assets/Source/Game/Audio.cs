using UnityEngine;

public class Audio
{
    public string Name;
    public AudioClip Clip;

    public Audio(string name, AudioClip clip) {
        Name = name;
        Clip = clip;
    }
}