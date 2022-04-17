using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float minRotation, maxRotation;
    [SerializeField] private int numOfBullets;
    [SerializeField] private bool isRandom;
    [SerializeField] private float rotateSpeed;

    [SerializeField] private float cooldown;
    private float timer;
    [SerializeField] private float bulletLifetime;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private Vector2 bulletVelocity;

    private float[] rotations;

    private void Start() {
        timer = cooldown;
        rotations = new float[numOfBullets];
        RandomRotations();
        if (!isRandom) {
            DistributedRotations();
        }
    }

    void Update() {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        if (timer <= 0) {
            SpawnBullets();
            timer = cooldown;
        }
        timer -= Time.deltaTime;
    }

    public float[] RandomRotations() {
        for (int i = 0; i < numOfBullets; i++) {
            rotations[i] = Random.Range(minRotation, maxRotation);
        }
        return rotations;
    }

    public float[] DistributedRotations() {
        for (int i = 0; i < numOfBullets; i++) {
            var fraction = (float)i / ((float)numOfBullets - 1);
            var difference = maxRotation - minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference;
        }
        return rotations;
    }

    public GameObject[] SpawnBullets() {
        if(isRandom) {
            RandomRotations();
        }

        GameObject[] spawnedBullets = new GameObject[numOfBullets];
        for (int i = 0; i < numOfBullets; i++) {
            spawnedBullets[i] = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var b = spawnedBullets[i].GetComponent<Bullet>();
            b.rotation = rotations[i];
            b.speed = bulletSpeed;
            b.velocity = bulletVelocity;
            b.Lifetime(bulletLifetime);
        }
        return spawnedBullets;
    }
}
