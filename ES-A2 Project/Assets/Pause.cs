using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour {

    [SerializeField] private GameObject pausePanel;
    private bool paused;

    // Use this for initialization
    void Start () {
        paused = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("p") && !paused)
        {
            Time.timeScale = 0;
            pausePanel.SetActive(true);
            paused = true;
            Debug.Log("paused");
        }

        if (Time.timeScale != 0 && paused)
        {
            paused = false;
            pausePanel.SetActive(false);
        }


    }
}
