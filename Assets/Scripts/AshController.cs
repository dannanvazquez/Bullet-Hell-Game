using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AshController : MonoBehaviour
{
    [SerializeField] private GameObject spawnerPrefab;

    [Header("Ability 1")]
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _minRotation, _maxRotation;
    [SerializeField] private int _numOfBullets;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Vector2 _bulletVelocity;
    [SerializeField] private float ability1Cooldown;
    private float ability1Timer;

    [Header("Ability 2")]
    [SerializeField] private GameObject flowerPrefab;
    [SerializeField] private Vector3 leftFlowerSpawn;
    private GameObject leftFlower = null;
    [SerializeField] private Vector3 rightFlowerSpawn;
    private GameObject rightFlower = null;
    [SerializeField] private float ability2Cooldown;
    private float ability2Timer;
    [SerializeField] private GameObject waterBullet;
    [HideInInspector] public bool isForcefield;
    private bool isActiveAbility2 = false;
    private bool isRotate;

    private void Start() {
        ability1Timer = ability1Cooldown;
        ability2Timer = 5;
    }

    private void Update() {
        if(isForcefield && leftFlower == null && rightFlower == null) {
            isForcefield = false;
            transform.Find("Forcefield").gameObject.SetActive(false);
            isActiveAbility2 = false;
        }
        if (ability2Timer <= 0 && isActiveAbility2 == false) {
            isActiveAbility2 = true;
            if (Random.Range(0,2) == 3) {
                StartCoroutine(LeftSpawnRain());
            } else {
                StartCoroutine(RightSpawnRain());
            }
            StartCoroutine(SpawnFlowers());
            ability2Timer = ability2Cooldown;
        } else if (ability1Timer <= 0 && isActiveAbility2 == false) {
            var s = Instantiate(spawnerPrefab, transform.position, Quaternion.identity);
            var bs = s.GetComponent<BulletSpawner>();
            bs.bulletPrefab = _bulletPrefab;
            if (isRotate) {
                bs.minRotation = _minRotation + 22.5f;
                bs.maxRotation = _maxRotation + 22.5f;
                isRotate = false;
            } else {
                bs.minRotation = _minRotation;
                bs.maxRotation = _maxRotation;
                isRotate = true;
            }
            bs.numOfBullets = _numOfBullets;
            bs.rotateSpeed = _rotateSpeed;
            bs.bulletSpeed = _bulletSpeed;
            bs.bulletVelocity = _bulletVelocity;
            bs.pulseAmount = 1;
            ability1Timer = ability1Cooldown;
        }
        ability1Timer -= Time.deltaTime;
        ability2Timer -= Time.deltaTime;
    }

    IEnumerator LeftSpawnRain() {
        for (int i = 0; i < 8; i++) {
            var s = Instantiate(spawnerPrefab, new Vector3(-15 + (i * 3), 6, 0), Quaternion.identity);
            var bs = s.GetComponent<BulletSpawner>();
            bs.bulletPrefab = waterBullet;
            bs.minRotation = 300;
            bs.maxRotation = 301;
            bs.rotateSpeed = 0;
            bs.numOfBullets = 1;
            bs.cooldown = 1f;
            bs.pulseAmount = 20;
            bs.bulletSpeed = 8;
        }
        yield return new WaitForSeconds(0.5f);
        for (int i = 0; i < 8; i++) {
            var s = Instantiate(spawnerPrefab, new Vector3(-13.5f + (i * 3), 6, 0), Quaternion.identity);
            var bs = s.GetComponent<BulletSpawner>();
            bs.bulletPrefab = waterBullet;
            bs.minRotation = 300;
            bs.maxRotation = 301;
            bs.rotateSpeed = 0;
            bs.numOfBullets = 1;
            bs.cooldown = 1f;
            bs.pulseAmount = 20;
            bs.bulletSpeed = 8;
        }
        yield return null;
    }

    IEnumerator RightSpawnRain() {
        for (int i = 0; i < 8; i++) {
            var s = Instantiate(spawnerPrefab, new Vector3(15 - (i * 3), 6, 0), Quaternion.identity);
            var bs = s.GetComponent<BulletSpawner>();
            bs.bulletPrefab = waterBullet;
            bs.minRotation = 240;
            bs.maxRotation = 241;
            bs.rotateSpeed = 0;
            bs.numOfBullets = 1;
            bs.cooldown = 1f;
            bs.pulseAmount = 20;
            bs.bulletSpeed = 8;
        }
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 8; i++) {
            var s = Instantiate(spawnerPrefab, new Vector3(13.5f - (i * 3), 6, 0), Quaternion.identity);
            var bs = s.GetComponent<BulletSpawner>();
            bs.bulletPrefab = waterBullet;
            bs.minRotation = 240;
            bs.maxRotation = 241;
            bs.rotateSpeed = 0;
            bs.numOfBullets = 1;
            bs.cooldown = 1f;
            bs.pulseAmount = 20;
            bs.bulletSpeed = 8;
        }
        yield return null;
    }

    IEnumerator SpawnFlowers() {
        yield return new WaitForSeconds(1.5f);
        leftFlower = Instantiate(flowerPrefab, leftFlowerSpawn, Quaternion.identity);
        rightFlower = Instantiate(flowerPrefab, rightFlowerSpawn, Quaternion.identity);
        yield return new WaitForSeconds(5f);
        transform.Find("Forcefield").gameObject.SetActive(true);
        isForcefield = true;
        leftFlower.GetComponent<Animator>().SetTrigger("Grow");
        rightFlower.GetComponent<Animator>().SetTrigger("Grow");
        yield return new WaitForSeconds(5f);
        leftFlower.transform.Find("FlowerPattern").gameObject.SetActive(true);
        leftFlower.GetComponent<FlowerController>().enabled = true;
        rightFlower.transform.Find("FlowerPattern").gameObject.SetActive(true);
        rightFlower.GetComponent<FlowerController>().enabled = true;
        //rightFlower.transform.Find("FlowerPattern").gameObject.GetComponent<FlowerPatternSpawner>().rotateSpeed *= -1;
        yield return new WaitForSeconds(30f);
        int heal = 0;
        if (leftFlower != null) {
            heal += 30;
        }
        if (rightFlower != null) {
            heal += 30;
        }
        Boss boss = gameObject.gameObject.GetComponent<Boss>();
        if (boss.health + heal > boss.maxHealth) {
            boss.health = boss.maxHealth;
        } else {
            boss.health += heal;
        }
        boss.healthBar.value = (float)boss.health / (float)boss.maxHealth;
        Destroy(leftFlower);
        Destroy(rightFlower);
        transform.Find("Forcefield").gameObject.SetActive(false);
        isForcefield = false;
        isActiveAbility2 = false;
        yield return null;
    }
}
