using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void AddCharacter()
    {
        CharacterMenu.Select = 1;
        Debug.Log(CharacterMenu.Select);
    }
    public void GoBackVariablesOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ContinueVariablesOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(3);
    }

    public void GoBackCharacterOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void ContinueCharacterOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
    }

    public void PlayIndexOnClick()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(2);
    }

    public void OptionsIndexOnClick()
    {
        //Application.LoadLevel(2);
    }

    public void HelpIndexOnClick()
    {
        //Application.LoadLevel(3);
    }

    public void ExitIndexOnClick()
    {
        Application.Quit();
    }
}
