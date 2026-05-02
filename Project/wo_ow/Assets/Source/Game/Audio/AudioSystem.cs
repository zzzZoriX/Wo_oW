using System;
using TMPro;
using UnityEngine;

public class AudioSystem : MonoBehaviour
{
    private static GameObject _instance;

    [SerializeField] private AudioSource source;
    [SerializeField] private AudioSwitcher switcher;
    [SerializeField] private TextMeshProUGUI currentlyPlaying;
    [SerializeField] private TextMeshProUGUI audioDuration;
    [SerializeField] private float hueLimit;
    [SerializeField] private float rainbowSpeed;

    private float _audioDuration = 0, _audioLength;
    private float _hue = 0f;


    private void Awake() {
        if (_instance == null) {
            DontDestroyOnLoad(gameObject);
            _instance = gameObject;
        }
        else {
            Destroy(gameObject);
        }
    }
    
    private void Update() {
        HandleAudioSwitch();
        SetVolume();
    }

    private void HandleAudioSwitch() {
        if (_audioDuration >= _audioLength) {
            var generatedAudio = switcher.GenerateAudio();
            _audioDuration = 0;
            _audioLength = generatedAudio.Clip.length;

            source.clip = generatedAudio.Clip;

            currentlyPlaying.text = "Currently playing: " + generatedAudio.Name;
            
            source.Play();
        }
        else {
            _audioDuration += Time.deltaTime;
        }
        
        DoRainbow();

        var length = TimeSpan.FromSeconds(_audioLength);
        var remaining = TimeSpan.FromSeconds(_audioDuration);

        audioDuration.text = string.Format("{0:D2}:{1:D2}", remaining.Minutes, remaining.Seconds) +
                             " / " +
                             string.Format("{0:D2}:{1:D2}", length.Minutes, length.Seconds);
    }

    private void DoRainbow() {
        _hue += Time.deltaTime * rainbowSpeed;

        if (_hue > hueLimit) {
            _hue -= hueLimit;
        }

        currentlyPlaying.color = Color.HSVToRGB(_hue, 1f, 1f);
        audioDuration.color = Color.HSVToRGB(_hue, 1f, 1f);
    }

    private void SetVolume() {
        source.volume = SettingsManager.Settings.Volume;
    }
}
