using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_ProjectileMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayOnClick()
	{
        SceneManager.LoadScene("Test Scene");
    }

    public void BackOnClick()
	{
        SceneManager.LoadScene("IndexMenu");
    }
}
