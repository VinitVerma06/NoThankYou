using System;
using UnityEngine;

public class GameHandler : MonoBehaviour {

    public static GameHandler Instance { get; private set; }

    public event EventHandler OnGameStateChanged;
    public event EventHandler OnGamePaused;
    public event EventHandler OnGameUnpaused;

    private enum State {
        WaitingToStart,
        CountdownToStart,
        GamePlaying,
        GameOver
    }

    private State state;

    private float countdownToStartTimer = 3f;
    private float gamePlayingTimer;
    private float gamePlayingTimerMax = 60f;


    public bool IsGamePaused = false;

    private void Awake() {
        Instance = this;
    }

    private void Start() {
        state = State.WaitingToStart;

        GameInput.Instance.OnGamePauseAction += GameInput_OnGamePauseAction;
    }

    private void GameInput_OnGamePauseAction(object sender, EventArgs e) {
        TogglePause();
    }

    private void Update() {
        switch (state) {
            case State.WaitingToStart:
                state = State.CountdownToStart;
                OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                break;

            case State.CountdownToStart:
                countdownToStartTimer -= Time.deltaTime;
                if (countdownToStartTimer < 0f) {
                    state = State.GamePlaying;
                    gamePlayingTimer = gamePlayingTimerMax;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GamePlaying:
                gamePlayingTimer -= Time.deltaTime;
                if (gamePlayingTimer < 0f) {
                    state = State.GameOver;
                    OnGameStateChanged?.Invoke(this, EventArgs.Empty);
                }
                break;

            case State.GameOver:
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                break;
        }
    }

    public void TogglePause() {
        IsGamePaused = !IsGamePaused;
        if (IsGamePaused) {
            OnGamePaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 0f;
        } else {
            OnGameUnpaused?.Invoke(this, EventArgs.Empty);
            Time.timeScale = 1f;
        }

        ToggleCursor();
    }

    private void ToggleCursor() {
        if (IsGamePaused) {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        } else {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }


    public float GetCountdownToStartTime() {
        return countdownToStartTimer;
    }

    public float GetGamePlayingTimeNormalized() {
        return 1 - (gamePlayingTimer / gamePlayingTimerMax);
    }

    public bool IsWaitingToStart() {
        return state == State.WaitingToStart;
    }

    public bool IsCountdownToStart() {
        return state == State.CountdownToStart;
    }

    public bool IsGamePlaying() {
        return state == State.GamePlaying;
    }

    public bool GetIsGamePaused() {
        return IsGamePaused;
    }

    public bool IsGameOver() {
        return state == State.GameOver;
    }

    private void OnDestroy() {
        GameInput.Instance.OnGamePauseAction -= GameInput_OnGamePauseAction;
    }
}
