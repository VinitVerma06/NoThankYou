using UnityEngine;
using UnityEngine.UI;

public class PauseUI : MonoBehaviour {

    [SerializeField] private Button resumeButton;
    [SerializeField] private Button restartButton;
    [SerializeField] private Button mainMenuButton;



    private void Awake() {
        resumeButton.onClick.AddListener(() => {
            GameHandler.Instance.TogglePause();
        });

        restartButton.onClick.AddListener(() => {
            Time.timeScale = 1f;
            Loader.Load(Loader.Scene.GameScene);
        });

        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
        });
    }

    private void Start() {
        Hide();

        GameHandler.Instance.OnGamePaused += GameHandler_OnGamePaused;
        GameHandler.Instance.OnGameUnpaused += GameHandler_OnGameUnpaused;
    }


    private void GameHandler_OnGamePaused(object sender, System.EventArgs e) {
        Show();
    }


    private void GameHandler_OnGameUnpaused(object sender, System.EventArgs e) {
        Hide();
    }


    private void OnDestroy() {
        GameHandler.Instance.OnGamePaused -= GameHandler_OnGamePaused;
        GameHandler.Instance.OnGameUnpaused -= GameHandler_OnGameUnpaused;
    }

    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
