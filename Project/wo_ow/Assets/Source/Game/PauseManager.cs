using UnityEngine;

public class PauseManager : MonoBehaviour {
    public bool GamePaused { get; private set; }
    public static PauseManager Instance { get; private set; }

    private GameManager gameManager;


    private void Start() {
        if (Instance != null && Instance != this) {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        
        DontDestroyOnLoad(gameObject);


        gameManager = GameManager.Instance;
    }

    public void Resume() {
        if (!GamePaused)
            return;

        GamePaused = false;

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void Pause() {
        GamePaused = true;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}