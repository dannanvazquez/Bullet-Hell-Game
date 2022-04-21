using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JerryController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private GameObject walkableArea;
    private Vector3 targetLocation;
    [SerializeField] private GameObject spawnerPrefab;

    [Header("Ability 1")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _minRotation, _maxRotation;
    [SerializeField] private int _numOfBullets;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Vector2 _bulletVelocity;
    [SerializeField] private float bulletCooldown;
    private float bulletTimer;

    private void Start() {
        walkableArea = GameObject.Find("/WalkableArea");
        targetLocation = transform.position;
        bulletTimer = bulletCooldown;
    }

    void Update() {
        float widthArea = walkableArea.GetComponent<SpriteRenderer>().bounds.size.x;
        float heightArea = walkableArea.GetComponent<SpriteRenderer>().bounds.size.y;
        if (targetLocation.x - transform.position.x <= 0.1f && targetLocation.y - transform.position.y <= 0.1f) {
            targetLocation.x = Random.Range(walkableArea.transform.position.x - (widthArea / 2), walkableArea.transform.position.x + (widthArea /2));
            targetLocation.y = Random.Range(walkableArea.transform.position.y - (heightArea / 2), walkableArea.transform.position.y + (heightArea / 2));
        }
        transform.position = Vector3.MoveTowards(transform.position, targetLocation, speed * Time.deltaTime);
        if (bulletTimer <= 0) {
            GameObject spawner = Instantiate(spawnerPrefab, transform.position, Quaternion.identity);
            var bs = spawner.GetComponent<BulletSpawner>();
            bs.bulletPrefab = _bulletPrefab;
            bs.minRotation = _minRotation;
            bs.maxRotation = _maxRotation;
            bs.numOfBullets = _numOfBullets;
            bs.rotateSpeed = _rotateSpeed;
            bs.bulletSpeed = _bulletSpeed;
            bs.bulletVelocity = _bulletVelocity;
            bulletTimer = bulletCooldown;
        }
        bulletTimer -= Time.deltaTime;
    }
}
