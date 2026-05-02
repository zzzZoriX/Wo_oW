using System.Collections.Generic;
using UnityEngine;


public class AudioPlayer : MonoBehaviour
{
    public Dictionary<string, AudioClip> AvailableClips { get; private set; }

    [SerializeField] private AudioSource source;
    private static GameObject _instance; 


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
        }
        else {
            Destroy(gameObject);
        }
    }

    public static AudioPlayer GetPlayer() => AudioPlayer._instance.GetComponent<AudioPlayer>();

    public void PlayOnce(AudioClip clip)
        => source.PlayOneShot(clip);
}
