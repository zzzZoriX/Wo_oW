using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    public static Settings Settings { get; private set; }
    public static GameObject Canvas { get; private set; }

    [SerializeField] private SettingsUIObjects uiObjects;
    private static GameObject _instance;


    private void Awake() {
        if (_instance == null) {
            _instance = gameObject;
            
            DontDestroyOnLoad(gameObject);

            SettingsManager.Canvas = uiObjects.MainCanvas;

            SettingsManager.Settings = new Settings();
        }
        else {
            Destroy(gameObject);
        }

        SettingsManager.Settings = new Settings();
    }

    private void Update() {
        UpdateSensitivity();
        UpdateVolume();
        UpdateSfxVolume();
    }

    private void UpdateSensitivity() {
        SettingsManager.Settings.Sensitivity = uiObjects.Sensitivity.value * 2;

        uiObjects.SensitivityText.text = Math.Round(SettingsManager.Settings.Sensitivity, 2).ToString();
    }

    private void UpdateVolume() {
        SettingsManager.Settings.Volume = uiObjects.Volume.value / 2;

        uiObjects.VolumeText.text = Math.Round(uiObjects.Volume.value, 2).ToString();
    }

    private void UpdateSfxVolume() {
        SettingsManager.Settings.SFXVolume = uiObjects.SFXVolume.value / 2;

        uiObjects.SFXVolumeText.text = Math.Round(uiObjects.SFXVolume.value, 2).ToString();
    }

    public void ExitSettings() {
        SettingsManager.Canvas.SetActive(false);
    }
}