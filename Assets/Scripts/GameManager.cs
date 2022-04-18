using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private float spawnCooldown;
    private float spawnTimer;
    private int stage = 0;
    private GameObject currentBoss = null;

    [SerializeField] private GameObject bossBar;
    [SerializeField] private Text bossNameText;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject jerryBoss;

    private void Start() {
        spawnTimer = spawnCooldown;
        countdown.GetComponent<Text>().text = spawnTimer.ToString("F0");
        countdown.SetActive(true);
    }

    private void Update() {
        if (currentBoss == null) {
            if (spawnTimer <= 0) {
                if (stage == 0) {
                    countdown.SetActive(false);
                    currentBoss = Instantiate(jerryBoss, Vector3.zero, Quaternion.identity);
                    bossNameText.text = currentBoss.GetComponent<Boss>().bossName;
                    bossHealthSlider.value = currentBoss.GetComponent<Boss>().health;
                    bossBar.SetActive(true);
                }
            }
            spawnTimer -= Time.deltaTime;
            countdown.GetComponent<Text>().text = spawnTimer.ToString("F0");
        } else {
            if(currentBoss.GetComponent<Boss>().health <= 0) {
                Destroy(currentBoss);
                bossBar.SetActive(false);
                stage++;
                spawnTimer = spawnCooldown;
                countdown.GetComponent<Text>().text = spawnTimer.ToString("F0");
                countdown.SetActive(true);
            }
        }
    }
}
