using UnityEngine;
using Unity.Cinemachine;

public class CinemachineCameraGroundClamp : CinemachineExtension {

    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float heightAboveGround = 0.5f;

    // How far above the camera the ray starts from.
    // Needs to be high enough that even when the camera is underground
    // the ray origin is still above the surface.
    [SerializeField] private float raycastOriginOffset = 10f;

    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage,
        ref CameraState state,
        float deltaTime) {

        if (stage == CinemachineCore.Stage.Finalize) {

            Vector3 cameraPos = state.GetFinalPosition();

            // Start the ray well ABOVE the camera position so it always
            // begins above the ground, even if the camera has gone below it
            Vector3 rayOrigin = new Vector3(cameraPos.x, cameraPos.y + raycastOriginOffset, cameraPos.z);

            if (Physics.Raycast(
                    rayOrigin,
                    Vector3.down,
                    out RaycastHit hit,
                    raycastOriginOffset + 1f,   // only search as far as needed
                    groundLayer)) {

                float minimumY = hit.point.y + heightAboveGround;

                if (cameraPos.y < minimumY) {
                    cameraPos.y = minimumY;
                    state.RawPosition = cameraPos;
                }
            }
        }
    }
}