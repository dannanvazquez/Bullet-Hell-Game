using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject continueMenu;

    public void StartGame() {
        if (PlayerPrefs.GetInt("bossStage",0) > 0) {
            mainMenu.SetActive(false);
            continueMenu.SetActive(true);
        } else {
            NewGame();
        }
    }

    public void QuitGame() {
        Application.Quit();
    }

    public void NewGame() {
        PlayerPrefs.SetInt("bossStage", 0);
        LoadScene("Game");
    }

    public void LoadScene(string sceneName) {
        SceneManager.LoadScene(sceneName);
    }
}
