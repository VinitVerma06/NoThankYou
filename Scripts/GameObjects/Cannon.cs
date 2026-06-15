using System;
using UnityEngine;

public class Cannon : MonoBehaviour {

    private const string PROJECTILE_LAYER = "Projectile";

    [SerializeField] private Transform projectileSpawn;
    [SerializeField] private GameObject projectile;

    private float minForce = 14f;
    private float maxForce = 17f;
    private float sprayAngle = 0.02f;

    private bool hasFired;


    private void Start() {
        hasFired = false;

        Projectile.OnProjectileDestroyed += Projectile_OnProjectileDestroyed;
    }

    private void Projectile_OnProjectileDestroyed() {
        hasFired = false;
    }


    public void Shoot() {

        if (hasFired) return;

        // Gets a sprayed direction to shoot at
        Vector3 shootDirection = GetSprayDirection(projectileSpawn);

        float force = UnityEngine.Random.Range(minForce, maxForce);

        GameObject currentProjectile = Instantiate(
            projectile, 
            projectileSpawn.position, 
            projectileSpawn.rotation
        );


        Rigidbody rb = currentProjectile.GetComponent<Rigidbody>();
        rb.AddForce(shootDirection * force, ForceMode.Impulse);

        int projectileLayer = LayerMask.NameToLayer(PROJECTILE_LAYER);
        currentProjectile.layer = projectileLayer;

        Projectile projectileScript = currentProjectile.GetComponent<Projectile>();
        projectileScript.Initialize(this);
        
        hasFired = true;
    }


    private Vector3 GetSprayDirection(Transform barrel) {
        Vector2 randomCircle = UnityEngine.Random.insideUnitCircle;

        Vector3 localSprayDir = new Vector3(
            randomCircle.x * sprayAngle,
            randomCircle.y * sprayAngle,
            1f
        );

        return barrel.TransformDirection(localSprayDir.normalized);
    }


    private void OnDestroy() {
        Projectile.OnProjectileDestroyed -= Projectile_OnProjectileDestroyed;
    }

}
