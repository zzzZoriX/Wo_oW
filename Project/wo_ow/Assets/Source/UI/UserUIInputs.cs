using UnityEngine;

public class UserUIInputs : MonoBehaviour {
    [SerializeField] private GameManager gameManager;
    private bool _gamePaused = false;
    
    
    private void Update() {
        Pause();
        QuitGame();
    }

    private void Pause() {
        if(!Input.GetKeyDown(KeyCode.Escape))
            return;
        
        switch (_gamePaused) {
            case true:
                gameManager.ResumeGame();
                break;
            
            case false:
                gameManager.FreezeGame();
                break;
        }

        _gamePaused = !_gamePaused;
    }

    private void QuitGame() {
        if (!Input.GetKeyDown(KeyCode.Q) || !_gamePaused)
            return;
        
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }
}