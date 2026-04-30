using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioSwitcher : MonoBehaviour
{
    [SerializeField] private AudioSource source;

    private List<Audio> _audioList;

    private int _lastLoaded = -1;
    private float _audioDuration = 0;


    private void Awake() {
        _audioList = new List<Audio> {
            new Audio("TOKYOPILL - death2007", Resources.Load<AudioClip>("Music/gs1")),
            new Audio("ctrlmania - mental entrapment", Resources.Load<AudioClip>("Music/gs2")),
            new Audio("SHADXWBXRN - \"DYNAMIC\"", Resources.Load<AudioClip>("Music/gs3")),
            new Audio("Desx - No Filter", Resources.Load<AudioClip>("Music/gs4"))
        };    
    }

    private void Update() {
        HandleAudioSwitch();
    }

    private void HandleAudioSwitch() {
        if (_audioDuration <= 0) {
            var audio = GenerateAudio();
            _audioDuration = audio.Clip.length;

            source.clip = audio.Clip;

            // TODO: do show which sound currently playing
            
            source.Play();
        }
        else {
            _audioDuration -= Time.deltaTime;
        }
    }

    private Audio GenerateAudio() {
        var index = -1;

        do {
            index = Random.Range(0, _audioList.Count);
        } while (index == _lastLoaded);

        _lastLoaded = index;

        return _audioList[index];
    }
}
