using UnityEngine;
using UnityEngine.SceneManagement;

public class Btn_MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayOnClick()
	{
		SceneManager.LoadScene("ProjectilesMenu");
	}

	public void OptionsOnClick()
	{
        //SceneManager.LoadScene("");
	}

	public void HelpOnClick()
	{
        //SceneManager.LoadScene("");
    }

    public void ExitOnClick()
	{
		Application.Quit();
	}
}
