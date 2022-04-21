using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlowerController : MonoBehaviour
{
    public GameObject healthBar;
    [SerializeField] private Slider healthSlider;
    [SerializeField] private int maxHealth;
    [SerializeField] private int health;

    void Start()
    {
        health = maxHealth;
        healthSlider.value = 1;
        healthBar.SetActive(true);
        this.GetComponent<BoxCollider2D>().enabled = true;
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.tag == "PlayerBullet") {
            health -= 1;
            healthSlider.value = (float)health / (float)maxHealth;
            if (health <= 0) {
                Destroy(gameObject);
            }
        }
    }
}
