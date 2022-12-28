using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OmbreController : MonoBehaviour
{
    [SerializeField] private AudioClip musicIntroClip;
    [SerializeField] private AudioClip musicLoopClip;
    [SerializeField] private Sprite neutralBoss;
    [SerializeField] private Sprite scareBoss;

    [Header("Ability 1")]
    [SerializeField] private float minTeleportTime;
    [SerializeField] private float maxTeleportTime;
    [SerializeField] private Vector3 locationA, locationB, locationC, locationD;
    private float ability1Timer = 0.0f;

    [SerializeField] private GameObject spawnerPrefab;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private float _minRotation, _maxRotation;
    [SerializeField] private int _numOfBullets;
    [SerializeField] private float _rotateSpeed;
    [SerializeField] private float _bulletSpeed;
    [SerializeField] private Vector2 _bulletVelocity;

    private bool isLeft;

    private void Start() {
        isLeft = (Random.Range(0, 2) == 0) ? true : false;
        ability1Timer = minTeleportTime;
        StartCoroutine(PlayMusic());
    }

    private void Update() {
        if (ability1Timer <= 0) {
            int randomLoc = Random.Range(0, 2);
            if (isLeft) {
                if (randomLoc == 0) {
                    transform.position = locationA;
                } else {
                    transform.position = locationC;
                }
                isLeft = false;
            } else {
                if (randomLoc == 0) {
                    transform.position = locationB;
                } else {
                    transform.position = locationD;
                }
                isLeft = true;
            }
            int randomSpawnTime = Random.Range((int)minTeleportTime, (int)maxTeleportTime);
            ability1Timer = randomSpawnTime;
            StartCoroutine(SpawnGhostBullets(randomSpawnTime * 2));
            StartCoroutine(ScarePlayer());
        }
        ability1Timer -= Time.deltaTime;
    }

    IEnumerator PlayMusic() {
        AudioSource AS = GetComponent<AudioSource>();
        AS.clip = musicIntroClip;
        AS.volume = 0.5f;
        AS.Play();
        yield return new WaitForSeconds(musicIntroClip.length);
        AS.clip = musicLoopClip;
        AS.Play();
        AS.loop = true;
    }

    IEnumerator ScarePlayer() {
        gameObject.GetComponent<SpriteRenderer>().sprite = scareBoss;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = neutralBoss;
    }

    IEnumerator SpawnGhostBullets(int spawnAmount) {
        for (int i = 0; i < spawnAmount; i++) {
            GameObject spawner = Instantiate(spawnerPrefab, transform.position, Quaternion.identity);
            var bs = spawner.GetComponent<BulletSpawner>();
            bs.bulletPrefab = _bulletPrefab;
            bs.minRotation = _minRotation + (i*10);
            bs.maxRotation = _maxRotation + (i*10);
            bs.numOfBullets = _numOfBullets;
            bs.rotateSpeed = _rotateSpeed;
            bs.bulletSpeed = _bulletSpeed;
            bs.bulletVelocity = _bulletVelocity;
            yield return new WaitForSeconds(0.5f);
        }
    }
}
