using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    public GameObject bulletPrefab;
    public float minRotation, maxRotation;
     public int numOfBullets;
     public float rotateSpeed;
     public float bulletSpeed;
     public Vector2 bulletVelocity;

    public float cooldown;
    private float timer;
    public int pulseAmount;

    public float lifetimeCooldown;
    private float lifetimeTimer;

    private float[] rotations;

    private void Start() {
        timer = cooldown;
        lifetimeTimer = lifetimeCooldown;
        rotations = new float[numOfBullets];
        DistributedRotations();
        SpawnBullets();
    }

    private void Update() {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        if (timer <= 0 && pulseAmount > 0) {
            SpawnBullets();
        }
        if (pulseAmount <= 0) {
            lifetimeTimer -= Time.deltaTime;
        }
        if (lifetimeTimer <= 0) {
            Destroy(gameObject);
        }

        timer -= Time.deltaTime;
    }

    private float[] DistributedRotations() {
        for (int i = 0; i < numOfBullets; i++) {
            var fraction = ((float)i + minRotation) / ((float)numOfBullets);
            var difference = maxRotation - minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference;
        }
        return rotations;
    }

    private GameObject[] SpawnBullets() {
        pulseAmount--;
        timer = cooldown;
        GameObject[] spawnedBullets = new GameObject[numOfBullets];
        for (int i = 0; i < numOfBullets; i++) {
            spawnedBullets[i] = Instantiate(bulletPrefab, transform);
            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;
        }
        return spawnedBullets;
    }
}
