using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public string bossName;
    [SerializeField] private Slider healthBar;
    [SerializeField] private int maxHealth;
    public int health;

    void Start() {
        healthBar = GameObject.Find("/Canvas/BossBar/Health").GetComponent<Slider>();
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
        }
    }
}
