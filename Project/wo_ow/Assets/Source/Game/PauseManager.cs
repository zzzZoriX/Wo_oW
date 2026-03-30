using UnityEngine;

public class PauseManager : MonoBehaviour {
    public bool GamePaused { get; private set; }
    public static PauseManager Instance { get; private set; }

    [SerializeField] private GameObject pauseUI;
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

    private void Update() {
        Pause();
    }

    public void QuitGame() {
        if (!GamePaused)
            return;
        
        Application.Quit();
        
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void Resume() {
        if (!GamePaused)
            return;

        GamePaused = false;
        
        pauseUI.SetActive(false);

        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Pause() {
        if (!Input.GetKeyDown(KeyCode.Escape))
            return;

        GamePaused = true;
        
        pauseUI.SetActive(true);

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}