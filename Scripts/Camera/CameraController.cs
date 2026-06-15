using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour {

    [SerializeField] private GameInput gameInput;

    [SerializeField] private float cameraZoomSpeed = 2f;
    [SerializeField] private float cameraZoomLerpSpeed = 10f;
    [SerializeField] private float minCameraDistance = 3f;
    [SerializeField] private float maxCameraDistance = 10f;
    [SerializeField] private float scrollSensitivity = 0.5f;

    private CinemachineCamera cinemachineCamera;
    private CinemachineOrbitalFollow orbitalFollow;
    private CinemachineInputAxisController mouseInput;

    private float targetZoom;
    private float currentZoom;

    private void Awake() {
        cinemachineCamera = GetComponent<CinemachineCamera>();
        orbitalFollow = GetComponent<CinemachineOrbitalFollow>();
        mouseInput = GetComponent<CinemachineInputAxisController>();
        mouseInput.enabled = true;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Start() {
        targetZoom = currentZoom = orbitalFollow.Radius;

        GameHandler.Instance.OnGameStateChanged += GameHandler_OnGameStateChanged;
    }

    private void GameHandler_OnGameStateChanged(object sender, System.EventArgs e) {
        if(GameHandler.Instance.IsGameOver()) {
            mouseInput.enabled = false;
        }
    }

    private void Update() {
        HandleCameraZoom();
    }

    private void HandleCameraZoom() {
        // Zoom using mouse scroll
        float scrollDelta = gameInput.GetScrollDeltaNormalized();
        if (scrollDelta != 0) {
            targetZoom -= scrollDelta * cameraZoomSpeed * scrollSensitivity;
            targetZoom = Mathf.Clamp(targetZoom, minCameraDistance, maxCameraDistance);
        }

        // Zoom using gamepad bumpers
        float bumperDelta = gameInput.GetBumperDelta();
        if (bumperDelta != 0) {
            targetZoom -= bumperDelta * cameraZoomSpeed * scrollSensitivity;
            targetZoom = Mathf.Clamp(targetZoom, minCameraDistance, maxCameraDistance);
        }

        currentZoom = Mathf.Lerp(currentZoom, targetZoom, Time.deltaTime * cameraZoomLerpSpeed);
        orbitalFollow.Radius = currentZoom;
    }
    
}
