using TMPro;
using UnityEngine;

public class GameStartCountdownUI : MonoBehaviour {

    [SerializeField] private TextMeshProUGUI gameStartCountdownText;

    private int previousCountdownNumber = -1;

    private void Start() {
        Hide();
        GameHandler.Instance.OnGameStateChanged += GameHandler_OnGameStateChanged;
    }

    private void GameHandler_OnGameStateChanged(object sender, System.EventArgs e) {
        if (GameHandler.Instance.IsCountdownToStart()) {
            Show();
        } else {
            Hide();
        }
    }

    private void Update() {
        int countdownNumber = Mathf.CeilToInt(GameHandler.Instance.GetCountdownToStartTime());
        gameStartCountdownText.text = countdownNumber.ToString();

        if (previousCountdownNumber != countdownNumber) {
            previousCountdownNumber = countdownNumber;

        }
    }

    private void OnDestroy() {
        GameHandler.Instance.OnGameStateChanged -= GameHandler_OnGameStateChanged;
    }


    private void Show() {
        gameObject.SetActive(true);
    }

    private void Hide() {
        gameObject.SetActive(false);
    }
}
