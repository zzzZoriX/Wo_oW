using System;
using UnityEngine;

public class SettingsManager : MonoBehaviour
{
    public static Settings Settings;

    [SerializeField] private SettingsUIObjects uiObjects;
    private static GameObject _instance;


    private void Awake() {
        if (_instance == null) {
            _instance = gameObject;
            
            DontDestroyOnLoad(gameObject);
        }
        else {
            Destroy(gameObject);
        }

        SettingsManager.Settings = new Settings();
    }

    private void Update() {
        UpdateSensitivity();
        UpdateVolume();
    }

    private void UpdateSensitivity() {
        SettingsManager.Settings.Sensitivity = uiObjects.Sensitivity.value * 2;

        uiObjects.SensitivityText.text = Math.Round(SettingsManager.Settings.Sensitivity, 2).ToString();
    }

    private void UpdateVolume() {
        SettingsManager.Settings.Volume = uiObjects.Volume.value / 2;

        uiObjects.VolumeText.text = Math.Round(uiObjects.Volume.value, 2).ToString();
    }
}