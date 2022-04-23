using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int spawnCooldown;
    private GameObject currentBoss = null;
    [SerializeField] private Vector3 spawnPos;
    [SerializeField] private AudioClip countdownClip;
    [SerializeField] private GameObject deathPanel;

    [SerializeField] private GameObject player;
    [SerializeField] private GameObject bossBar;
    [SerializeField] private Text bossNameText;
    [SerializeField] private Slider bossHealthSlider;
    [SerializeField] private GameObject countdown;
    [SerializeField] private GameObject jerryBoss;
    [SerializeField] private GameObject ashBoss;

    private void Awake() {
        StartCoroutine(ActivateCountdown());
    }

    private void Update() {
        if (player.GetComponent<Player>().health <= 0) {
            deathPanel.SetActive(true);
        }
        if (currentBoss != null) {
            if(currentBoss.GetComponent<Boss>().health <= 0) {
                PlayerPrefs.SetInt("bossStage", PlayerPrefs.GetInt("bossStage", 0) + 1);
                LoadScene("Game");
            }
        }
    }

    IEnumerator ActivateCountdown() {
        player.GetComponent<Player>().FullHeal();
        countdown.SetActive(true);
        countdown.GetComponent<Text>().text = spawnCooldown.ToString("F0");
        AudioSource audio = GetComponent<AudioSource>();
        audio.clip = countdownClip;
        audio.Play();
        yield return new WaitForSeconds(1f);
        for (int i = spawnCooldown - 1; i > 0; i--) {
            countdown.GetComponent<Text>().text = i.ToString("F0");
            yield return new WaitForSeconds(1f);
        }
        countdown.SetActive(false);
        SpawnBoss();
        yield return null;
    }

    private void SpawnBoss() {
        if (PlayerPrefs.GetInt("bossStage", 0) == 0) {
            currentBoss = Instantiate(jerryBoss, spawnPos, Quaternion.identity);
            bossNameText.text = currentBoss.GetComponent<Boss>().bossName;
            bossHealthSlider.value = currentBoss.GetComponent<Boss>().health;
            bossBar.SetActive(true);
        }
        else if (PlayerPrefs.GetInt("bossStage", 0) == 1) {
            currentBoss = Instantiate(ashBoss, spawnPos, Quaternion.identity);
            bossNameText.text = currentBoss.GetComponent<Boss>().bossName;
            bossHealthSlider.value = currentBoss.GetComponent<Boss>().health;
            bossBar.SetActive(true);
        }
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
