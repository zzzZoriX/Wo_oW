using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuUIManager : MonoBehaviour
{
    public static MainMenuUIManager Instance { get; private set; }

    [SerializeField] private GameObject settingsCanvas;


    private void Awake() {
        if (Instance == null) {
            Instance = gameObject.GetComponent<MainMenuUIManager>();
            
            settingsCanvas = SettingsManager.Canvas;
        }
        else {
            Destroy(gameObject);
        }
    }

    public void Exit() {
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Play() 
        => SceneManager.LoadScene("MainGame");

    public void Settings() {
        settingsCanvas.SetActive(true);
    }
}
