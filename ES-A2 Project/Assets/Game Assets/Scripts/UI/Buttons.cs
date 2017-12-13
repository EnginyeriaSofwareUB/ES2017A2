using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    public void ExitIndexOnClick() {
        Application.Quit();
    }

    public void PauseMenuContinueGame() {
        Time.timeScale = 1;
    }

    public void PauseMenuRestartGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("Test Scene");
    }
    public void PauseMenuExitGame() {
        Time.timeScale = 1;
        SceneManager.LoadScene("IndexMenu");
    }

    public void WinMenuRestartGame() {
        SceneManager.LoadScene("Test Scene");
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

    public void goToHelpMenu()
    {
        SceneManager.LoadScene("HelpMenu");
    }
}
