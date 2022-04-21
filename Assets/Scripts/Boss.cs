using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    public string bossName;
    [HideInInspector] public Slider healthBar;
    public int maxHealth;
    [HideInInspector] public int health;
    [SerializeField] private GameObject animatedObject;
    [SerializeField] private Sprite neutralAnimation;
    [SerializeField] private Sprite hurtAnimation;
    private float damageCooldown = 0.2f;
    private float damageTimer;

    void Start() {
        healthBar = GameObject.Find("/Canvas/BossBar/Health").GetComponent<Slider>();
        health = maxHealth;
        healthBar.value = 1;
    }

    void Update() {
        if (damageTimer <= 0) {
            animatedObject.GetComponent<SpriteRenderer>().sprite = neutralAnimation;
        }
        else {
            animatedObject.GetComponent<SpriteRenderer>().sprite = hurtAnimation;
            damageTimer -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "PlayerBullet") {
            if (gameObject.name == "AshTheDryad(Clone)") {
                if (gameObject.GetComponent<AshController>().isForcefield) {
                    if (health < maxHealth) {
                        health += 1;
                        healthBar.value = (float)health / (float)maxHealth;
                    }
                    damageTimer = damageCooldown;
                    Destroy(collision);
                    return;
                }
            }
            health -= 1;
            healthBar.value = (float)health / (float)maxHealth;
            damageTimer = damageCooldown;
            Destroy(collision);
        }
    }
}
