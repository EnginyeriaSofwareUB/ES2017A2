using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {
    public void GoBackVariablesOnClick() {
        previousScene();
    }

    public void ContinueVariablesOnClick() {
        Text coins = GameObject.Find("TextCounterMoney").GetComponent<Text>();
        int numCharacters = (int)GameObject.Find("SliderAnimals").GetComponent<Slider>().value;
        nextScene();
    }

    public void GoBackCharacterOnClick() {
        previousScene();
    }

    public void ContinueCharacterOnClick() {
        nextScene();
    }

    public void PlayIndexOnClick() {
        nextScene();
    }

    public void nextScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void previousScene() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OptionsIndexOnClick() {
        SceneManager.LoadScene("OptionsMenu");
    }
    public void MenuIndexOnClick() {
        SceneManager.LoadScene("IndexMenu");
    }

    public void HelpIndexOnClick() {
        //SceneManager.LoadScene("HelpScene");
    }

    public void ExitIndexOnClick() {
        Application.Quit();
    }

    public void PauseMenuContinueGame() {
        Time.timeScale = 1;
    }

    public void PauseMenuRestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("IndexMenu");
    }
    public void PauseMenuExitGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("IndexMenu");
    }

    public void WinMenuRestartGame() {
        SceneManager.LoadScene("IndexMenu");
    }
    public void WinMenuExitGame() {
        SceneManager.LoadScene("IndexMenu");
    }

    public void goToIndexMenu() {
        SceneManager.LoadScene("IndexMenu");
    }

    public void goToCharacterSelectMenu() {
        SceneManager.LoadScene("SelectCharacterMenu");
    }

    public void goToVariablesMenu() {
        SceneManager.LoadScene("VariablesMenu");
    }

    public void goToOptions() {
        SceneManager.LoadScene("OptionsMenu");
    }

    public void goToProjectilesSelectMenu() {
        SceneManager.LoadScene("ProjectilesMenu");
    }

    public void goToGameScene() {
        SceneManager.LoadScene("Test Scene");
    }

    public void goToWinScene() {
        SceneManager.LoadScene("winScene");
    }
}
