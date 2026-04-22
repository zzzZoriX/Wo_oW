using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UserUIManger : MonoBehaviour {
    [Header("UI Elements")] 
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject WeaponStatsUI;
    
    [Header("Death")]
    [SerializeField] private GameObject DieUI;
    [SerializeField] private TextMeshProUGUI TotalPoints;

    [Header("Objects")] 
    [SerializeField] private PlayerController player;
    [SerializeField] private PauseManager pauseManager;

    private bool _weaponStatsHiden;


    public void Quit() {
        if (!pauseManager.GamePaused)
            return;
        
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Menu()
        => SceneManager.LoadScene("MainMenu");

    public void Pause() {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;
        
        pauseManager.Pause();

        PauseUI.SetActive(true);
    }

    public void Resume() {
        if (!pauseManager.GamePaused)
            return;
        
        pauseManager.Resume();
        
        PauseUI.SetActive(false);
    }

    private void Update() {
        Pause();
        Die();
        HideNShotWeaponStats();
    }

    private void Die() {
        if (player.Health.IsAlive())
            return;

        pauseManager.Pause();
        
        DieUI.SetActive(true);

        TotalPoints.text = Math.Round(player.CoolPoints.Points).ToString();
    }

    private void HideNShotWeaponStats() {
        if (!Input.GetKeyDown(KeyCode.Tab))
            return;

        _weaponStatsHiden = !_weaponStatsHiden;
        
        WeaponStatsUI.SetActive(_weaponStatsHiden);
    }
}