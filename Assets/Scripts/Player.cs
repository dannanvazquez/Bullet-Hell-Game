using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth;
    [HideInInspector] public int health;
    [SerializeField] private float damageCooldown;
    private float damageTimer;
    [SerializeField] private float shootCooldown;
    private float shootTimer;
    [SerializeField] private AudioClip damageClip;

    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifetime;

    [SerializeField] private GameObject playerSprite;
    [SerializeField] private GameObject crosshairSprite;
    [SerializeField] private GameObject gunSprite;
    [SerializeField] private Transform bulletSpawn;

    private void Start()
    {
        health = maxHealth;
        healthBar.value = 1;
        Cursor.visible = false;
    }

    private void Update()
    {
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 10));
        float ySign = (mousePos.y < transform.position.y) ? -1f : 1f;
        playerSprite.GetComponent<SpriteRenderer>().flipX = (mousePos.x < transform.position.x) ? true : false;
        gunSprite.GetComponent<SpriteRenderer>().flipY = (mousePos.x < transform.position.x) ? true : false;
        bulletSpawn.localPosition = (mousePos.x < transform.position.x) ? new Vector3(bulletSpawn.localPosition.x, 0.4f, bulletSpawn.localPosition.z) : new Vector3(bulletSpawn.localPosition.x, -0.4f, bulletSpawn.localPosition.z);
        gunSprite.transform.rotation = Quaternion.Euler(0, 0, Vector2.Angle(Vector2.right, mousePos - transform.position) * ySign);
        crosshairSprite.transform.position = mousePos;
        if (health <= 0) {
            playerSprite.GetComponent<Animator>().SetTrigger("died");
            this.GetComponent<PlayerMovement>().enabled = false;
            this.enabled = false;
            return;
        }
        if(damageTimer <= 0) {
            Color opaque = playerSprite.GetComponent<SpriteRenderer>().color;
            opaque.a = 1f;
            playerSprite.GetComponent<SpriteRenderer>().color = opaque;
        } else {
            Color transparent = playerSprite.GetComponent<SpriteRenderer>().color;
            transparent.a = 0.5f;
            playerSprite.GetComponent<SpriteRenderer>().color = transparent;
            damageTimer -= Time.deltaTime;
        }
        if (Input.GetButton("Fire1") && shootTimer <= 0) {
            GameObject bullet = Instantiate(bulletPrefab, bulletSpawn.position, Quaternion.identity);
            var b = bullet.GetComponent<Bullet>();
            b.rotation = Vector2.Angle(Vector2.right, mousePos - bulletSpawn.position) * ySign;
            b.speed = bulletSpeed;
            Destroy(bullet, bulletLifetime);
            shootTimer = shootCooldown;
        } else {
            shootTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Bullet") && damageTimer <= 0) {
            AudioSource ASHit = gameObject.AddComponent<AudioSource>();
            ASHit.clip = damageClip;
            ASHit.volume = 0.5f;
            ASHit.Play();
            playerSprite.GetComponent<Animator>().SetTrigger("hit");
            health -= 1;
            healthBar.value = (float)health / (float)maxHealth;
            damageTimer = damageCooldown;
            Destroy(collision.gameObject);
        }
    }

    public void FullHeal() {
        health = maxHealth;
        healthBar.value = 1;
    }
}
