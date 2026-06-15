using System;
using UnityEngine;

public class Projectile : MonoBehaviour {

    public static event EventHandler OnGoalHit;

    public static event Action OnProjectileDestroyed;

    private Cannon parentCannon;
    private Rigidbody rigidBody;
    private bool collidedWithPlayer = false;
    private bool collidedWithGoalPost = false;

    private float maxLifeTime = 10f;
    private float despawnTime = 2f;
    private float despawnSpeedThreshold = 1f;


    private void Awake() {
        rigidBody = GetComponent<Rigidbody>();
        rigidBody.useGravity = false;

        Destroy(gameObject, maxLifeTime);   // Destroy itself after a duration
    }


    private void Update() {
        if (collidedWithPlayer && collidedWithGoalPost && rigidBody.linearVelocity.magnitude < despawnSpeedThreshold) {
            Invoke(nameof(DestroyProjectile), despawnTime);
        }
    }

    // Collision Detected : Collided with goal
    private void OnTriggerEnter(Collider other) {
        if (collidedWithGoalPost) return;

        if (other.TryGetComponent(out GoalTrigger goal)) { 

            OnGoalHit?.Invoke(this, EventArgs.Empty);

            Invoke(nameof(DestroyProjectile), despawnTime);
            rigidBody.useGravity = true;
            collidedWithGoalPost = true;
            return;
        }
    } 

    // Collision Detected : Collided with player
    private void OnCollisionEnter(Collision collision) {
        if (collidedWithPlayer) return;

        if (collision.gameObject.TryGetComponent(out Player player)) {
            rigidBody.useGravity = true;
            collidedWithPlayer = true;
            return;
        }
    }


    private void OnDestroy() {
        if (parentCannon != null) {
            OnProjectileDestroyed?.Invoke();
        }
    }

    // Destroy itself
    private void DestroyProjectile() {
        Destroy(gameObject);
    }


    public void Initialize(Cannon cannon) {
        parentCannon = cannon;
    }
}
