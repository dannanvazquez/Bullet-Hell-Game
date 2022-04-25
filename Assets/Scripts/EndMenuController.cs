using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EndMenuController : MonoBehaviour
{
    [SerializeField] private Text playtimeText;
    [SerializeField] private Text deathcountText;

    private void Awake() {
        playtimeText.text = "Time elapsed: " + (PlayerPrefs.GetFloat("playTime", 0) / 60).ToString("F0") + ":" + (PlayerPrefs.GetFloat("playTime", 0) % 60).ToString("00");
        deathcountText.text = "Deaths: " + PlayerPrefs.GetInt("deathCount", 0);
        PlayerPrefs.SetInt("bossStage", 0);
        PlayerPrefs.SetInt("playTime", 0);
        PlayerPrefs.SetInt("deathCount", 0);
    }

    public void MainMenu() {
        SceneManager.LoadScene("MainMenu");
    }
}
