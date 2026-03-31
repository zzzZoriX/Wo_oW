using UnityEngine;

public class UserUIManger : MonoBehaviour {
    [Header("UI Elements")] 
    [SerializeField] private GameObject PauseUI;
    [SerializeField] private GameObject DieUI;

    [Header("Objects")] 
    [SerializeField] private PlayerController player;
    [SerializeField] private PauseManager pauseManager;


    public void Quit() {
        if (!pauseManager.GamePaused)
            return;
        
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

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
    }

    private void Die() {
        if (player.Health.IsAlive())
            return;

        pauseManager.Pause();
        
        DieUI.SetActive(true);
    }
}