using UnityEngine;

public class BoundaryWall : MonoBehaviour {

    [SerializeField] private float pushBackForce = 5f;

    private void OnCollisionStay(Collision collision) {

        if (collision.gameObject.TryGetComponent(out Player player)) {

            Vector3 pushDirection = collision.contacts[0].normal;

            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null) {
                rb.AddForce(pushDirection * pushBackForce, ForceMode.Force);
            }
        }
    }
}