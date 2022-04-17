using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth;
    private int health;

    void Start() {
        health = maxHealth;
        healthBar.value = 1;
    }

    void Update() {
        if (health <= 0) {
            Destroy(gameObject);
            return;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "PlayerBullet") {
            health -= 1;
            healthBar.value = (float)health / (float)maxHealth;
            Destroy(collision.gameObject);
        }
    }
}
