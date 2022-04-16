using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private int maxHealth;
    private int health;
    [SerializeField] private float damageCooldown;
    private float damageTimer;
    [SerializeField] private float shootCooldown;
    private float shootTimer;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;

    [SerializeField] private GameObject face;
    [SerializeField] private Sprite neutralFace;
    [SerializeField] private Sprite hurtFace;
    [SerializeField] private Sprite deathFace;
    [SerializeField] private Sprite confusedFace;

    void Start()
    {
        health = maxHealth;
    }

    void Update()
    {
        if (health <= 0) {
            face.GetComponent<SpriteRenderer>().sprite = deathFace;
            this.GetComponent<PlayerMovement>().enabled = false;
            this.enabled = false;
            return;
        }
        if(damageTimer <= 0) {
            face.GetComponent<SpriteRenderer>().sprite = neutralFace;
        } else {
            face.GetComponent<SpriteRenderer>().sprite = hurtFace;
            damageTimer -= Time.deltaTime;
        }
        if (Input.GetButton("Fire1") && shootTimer <= 0) {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            var b = bullet.GetComponent<Bullet>();
            b.speed = bulletSpeed;
            shootTimer = shootCooldown;
        } else {
            shootTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "Bullet" && damageTimer <= 0) {
            health -= 1;
            damageTimer = damageCooldown;
        }
    }
}
