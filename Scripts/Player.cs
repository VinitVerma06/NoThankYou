using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour {

    [SerializeField] private GameInput gameInput;
    [SerializeField] private Transform spherecastTransform;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float jumpforce;


    private Rigidbody rigidBody;
    private bool isGrounded;

    private float groundCheckRadius = .4f;
    private float groundCheckDistance = .5f;


    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
    }


    private void Start() {
        GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
    }

    private void GameInput_OnJumpAction(object sender, System.EventArgs e) {
        HandleJump();
    }


    private void Update() {
        IsGrounded();
    }

    private void FixedUpdate() {
        PlayerMovement();
    }

    #region Player Movement 

    private float playerMovementSpeed = 7f;
    private float playerRotationSpeed = 10f;

    private void PlayerMovement() {
        Vector2 inputVector = gameInput.GetPlayerMovementNormalized();
        Vector3 inputDir = new Vector3(inputVector.x, 0f, inputVector.y);

        Vector3 cameraForward = cameraTransform.forward;
        Vector3 cameraRight = cameraTransform.right;

        cameraForward.y = cameraRight.y = 0f;

        cameraForward.Normalize();
        cameraRight.Normalize();

        Vector3 moveDir = (cameraForward * inputDir.z + cameraRight * inputDir.x);

        // Move by directly setting XZ velocity, preserving Y for gravity/jumping
        rigidBody.linearVelocity = new Vector3(moveDir.x * playerMovementSpeed, rigidBody.linearVelocity.y, moveDir.z * playerMovementSpeed);

        if (moveDir.sqrMagnitude > 0.001f) {
            Quaternion toRotation = Quaternion.LookRotation(moveDir, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, toRotation, playerRotationSpeed * Time.deltaTime);
        }
        
    }

    #endregion


    #region Player jump

    private void IsGrounded() {
        isGrounded = Physics.SphereCast(
            spherecastTransform.position,
            groundCheckRadius,
            Vector3.down,
            out RaycastHit hit,
            groundCheckDistance,
            groundLayer
        );
    }

    private void HandleJump() {
        if (isGrounded) {
            rigidBody.linearVelocity = new Vector3(rigidBody.linearVelocity.x, 0f, rigidBody.linearVelocity.z);
            rigidBody.AddForce(Vector3.up * jumpforce, ForceMode.Impulse);
        }
    }

    #endregion

    private void OnDestroy() {
        GameInput.Instance.OnJumpAction -= GameInput_OnJumpAction;
    }

    #region Draw Gizmos
    private void OnDrawGizmos() {
        // Calculate the exact same origin and end point used in CheckGrounded()
        Vector3 origin = spherecastTransform.position;
        Vector3 end = origin + Vector3.down * groundCheckDistance;

        // Color the gizmo based on grounded state so you can see it react live
        Gizmos.color = isGrounded ? Color.green : Color.red;

        // Draw the start sphere (where the cast begins)
        Gizmos.DrawWireSphere(origin, groundCheckRadius);

        // Draw the end sphere (the furthest point the sphere travels)
        Gizmos.DrawWireSphere(end, groundCheckRadius);

        // Draw a line connecting their centres so the sweep path is clear
        Gizmos.DrawLine(
            origin + Vector3.down * groundCheckRadius,
            end + Vector3.down * groundCheckRadius
        );
    }
    #endregion

}