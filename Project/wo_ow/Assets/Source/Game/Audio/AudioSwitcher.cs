using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioSwitcher : MonoBehaviour
{
    private List<Audio> _audioList;

    private int _lastLoaded = -1;


    private void Awake() {
        _audioList = new List<Audio> {
            new Audio("TOKYOPILL - death2007", Resources.Load<AudioClip>("Music/gs1")),
            new Audio("ctrlmania - mental entrapment", Resources.Load<AudioClip>("Music/gs2")),
            new Audio("SHADXWBXRN - \"DYNAMIC\"", Resources.Load<AudioClip>("Music/gs3")),
            new Audio("Desx - No Filter", Resources.Load<AudioClip>("Music/gs4"))
        };    
    }

    public Audio GenerateAudio() {
        var index = -1;

        do {
            index = Random.Range(0, _audioList.Count);
        } while (index == _lastLoaded);

        _lastLoaded = index;

        return _audioList[index];
    }
}
