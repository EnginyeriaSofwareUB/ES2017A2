﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Buttons : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayOnClick()
	{
		Application.LoadLevel(1);
	}

	public void OptionsOnClick()
	{
		//Application.LoadLevel(2);
	}

	public void HelpOnClick()
	{
		//Application.LoadLevel(3);
	}

	public void ExitOnClick()
	{
		Application.Quit();
	}

<<<<<<< HEAD
    public void AddCharacter()
    {
        CharacterMenu.Select = 1;
        Debug.Log(CharacterMenu.Select);
=======
    public void GoBackOnClick()
    {
        Application.LoadLevel(0);
>>>>>>> 0dd033567eba4e942c6363b2dbdc4d244b57bea9
    }
}
