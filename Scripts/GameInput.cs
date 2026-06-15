using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {

    public static GameInput Instance { get; private set; }

    public event EventHandler OnJumpAction;
    public event EventHandler OnGamePauseAction;

    private PlayerInputAction playerInputAction;

    private Vector2 scrollDelta;

    private void Awake() {
        Instance = this;
        playerInputAction = new PlayerInputAction();
    }
    private void OnEnable() {
        playerInputAction.Enable();

        playerInputAction.Player.Jump.performed += Jump_performed;
        playerInputAction.Player.Pause.performed += Pause_performed;
    }

    private void Pause_performed(InputAction.CallbackContext obj) {
        OnGamePauseAction?.Invoke(this, EventArgs.Empty);
    }

    private void Jump_performed(InputAction.CallbackContext obj) {
        OnJumpAction?.Invoke(this, EventArgs.Empty);
    }

    private void OnDisable() {
        playerInputAction.Player.Jump.performed -= Jump_performed;
        playerInputAction.Disable();
    }

    public float GetScrollDeltaNormalized() {
        float scrollValue = playerInputAction.CameraControl.MouseZoom.ReadValue<Vector2>().y;

        if (scrollValue > 0f) return 1f;
        if (scrollValue < 0f) return -1f;
        return 0f;

    }

    

    public float GetBumperDelta() {
        float bumperDelta = playerInputAction.CameraControl.GamepadZoom.ReadValue<float>();
        return bumperDelta;
    }

    public Vector2 GetPlayerMovementNormalized() {
        Vector2 gameInput = playerInputAction.Player.Move.ReadValue<Vector2>();
        return gameInput.normalized;
    }



}
