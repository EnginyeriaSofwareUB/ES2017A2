using System.Collections;
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
		Application.LoadLevel(0);
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

    public void GoBackOnClick()
    {
        Application.LoadLevel(2);
    }
}
