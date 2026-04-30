using System;
using TMPro;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    public static GameObject Instance;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSwitcher switcher;
    [SerializeField] private TextMeshProUGUI currentlyPlaying;

    private float _audioDuration = 0;


    private void Awake() {
        if (Instance == null) {
            DontDestroyOnLoad(gameObject);
            Instance = gameObject;
        }
        else {
            Destroy(gameObject);
        }
    }
    
    private void Update() {
        HandleAudioSwitch();
    }

    private void HandleAudioSwitch() {
        if (_audioDuration <= 0) {
            var generatedAudio = switcher.GenerateAudio();
            _audioDuration = generatedAudio.Clip.length;

            source.clip = generatedAudio.Clip;

            currentlyPlaying.text = "Currently playing: " + generatedAudio.Name;
            
            source.Play();
        }
        else {
            _audioDuration -= Time.deltaTime;
        }
    }
}
