using System.Collections.Generic;
using UnityEngine;


public class AudioPlayer : MonoBehaviour
{
    public Dictionary<string, AudioClip> AvailableClips { get; private set; }

    [SerializeField] private AudioSource source;
    private static GameObject _instance;

    private List<GameObject> _loopPlayers;


    private void Awake() {
        if (AudioPlayer._instance == null) {
            AudioPlayer._instance = gameObject;

            AvailableClips = new Dictionary<string, AudioClip>() {
                { "NSAttack", Resources.Load<AudioClip>("Music/sfx/neonsoldierattack") },
                { "LGAttack", Resources.Load<AudioClip>("Music/sfx/lasergunattack") },
                { "LGAbilityReady", Resources.Load<AudioClip>("Music/sfx/lasergunabilityready") },
                { "LGAbilityShot", Resources.Load<AudioClip>("Music/sfx/lasergunabiltyshot") },
                { "DeadByLG", Resources.Load<AudioClip>("Music/sfx/deadbylasergun") }
            };

            _loopPlayers = new List<GameObject>();
        }
        else {
            Destroy(gameObject);
        }
    }

    private void Update() {
        source.volume = SettingsManager.Settings.Volume;
        SetVolume();
    }

    public static AudioPlayer GetPlayer() => AudioPlayer._instance.GetComponent<AudioPlayer>();

    public void PlayOnce(AudioClip clip)
        => source.PlayOneShot(clip);

    public int PlayLoop(AudioClip clip) {
        var index = _loopPlayers.Count;
        
        _loopPlayers.Add(new GameObject());

        var loopSource = _loopPlayers[index].AddComponent<AudioSource>();

        loopSource.loop = true;
        loopSource.clip = clip;
        loopSource.volume = SettingsManager.Settings.SFXVolume;
        loopSource.Play();

        return index;
    }

    public void DestroyLoop(int index) {
        _loopPlayers[index].GetComponent<AudioSource>().Stop();
        
        _loopPlayers.RemoveAt(index);
    }

    public void SetVolume()
        => source.volume = SettingsManager.Settings.SFXVolume;
}
