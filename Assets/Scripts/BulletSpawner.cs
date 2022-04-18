using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletSpawner : MonoBehaviour
{
    [HideInInspector] public GameObject bulletPrefab;
    [HideInInspector] public float minRotation, maxRotation;
    [HideInInspector] public int numOfBullets;
    [HideInInspector] public bool isRandom;
    [HideInInspector] public float rotateSpeed;
    [HideInInspector] public float bulletSpeed;
    [HideInInspector] public Vector2 bulletVelocity;

    [SerializeField] private float timer;

    private float[] rotations;

    private void Start() {
        rotations = new float[numOfBullets];
        if (!isRandom) {
            DistributedRotations();
        } else {
            RandomRotations();
        }
        SpawnBullets();
    }

    private void Update() {
        transform.Rotate(0, 0, rotateSpeed * Time.deltaTime);
        if (timer <= 0) {
            Destroy(gameObject);
        }
        timer -= Time.deltaTime;
    }

    private float[] RandomRotations() {
        for (int i = 0; i < numOfBullets; i++) {
            rotations[i] = Random.Range(minRotation, maxRotation);
        }
        return rotations;
    }

    private float[] DistributedRotations() {
        for (int i = 0; i < numOfBullets; i++) {
            var fraction = (float)i / ((float)numOfBullets);
            var difference = maxRotation - minRotation;
            var fractionOfDifference = fraction * difference;
            rotations[i] = fractionOfDifference;
        }
        return rotations;
    }

    private GameObject[] SpawnBullets() {
        if(isRandom) {
            RandomRotations();
        }

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
