using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour {

    [SerializeField] private Button mainMenuButton;
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI gameOverFinalScoreText;

    private string gameOverScorePrefixText = "GOAL : ";


    private void Awake() {
        mainMenuButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.MainMenu);
        });

        replayButton.onClick.AddListener(() => {
            Loader.Load(Loader.Scene.GameScene);
        });
    }

    private void Start() {
        Hide();

        GameHandler.Instance.OnGameStateChanged += GameHandler_OnGameStateChanged;
    }

    private void GameHandler_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameHandler.Instance.IsGameOver()) {
            Show();
            UpdateFinalScore();
        } else {
            Hide();
        }
    }

    private void UpdateFinalScore() {
        gameOverFinalScoreText.text = gameOverScorePrefixText + GoalScoreUI.GetGoalScore().ToString();
    }


    private void Show() {
        gameObject.SetActive(true);
        replayButton.Select();
    }

    private void Hide() {
        gameObject.SetActive(false);
    }

    private void OnDestroy() {
        GameHandler.Instance.OnGameStateChanged -= GameHandler_OnGameStateChanged;
    }
}
