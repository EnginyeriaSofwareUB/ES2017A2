using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour {

    [SerializeField] private GameObject pausePanel;
    [SerializeField] private GameObject gamePanel;
    [SerializeField] private Image background;
    private int gameLayer;
    private bool paused;

    // Use this for initialization
    void Start() {
        paused = false;

    }

    // Update is called once per frame
    void Update() {
        if (Input.GetKeyDown(KeyCode.P) && !paused) {
            this.pause();
        }
        if (Time.timeScale != 0 && paused) {
            this.resume();
        }
    }

    private void pause() {
        gameLayer = gamePanel.layer;
        gamePanel.layer = 2;
        Time.timeScale = 0;
        this.background.enabled = true;
        pausePanel.SetActive(true);
        paused = true;
        Debug.Log("paused");
    }

    private void resume() {
        paused = false;
        pausePanel.SetActive(false);
        this.background.enabled = false;
        gamePanel.layer = gameLayer;
    }
}
