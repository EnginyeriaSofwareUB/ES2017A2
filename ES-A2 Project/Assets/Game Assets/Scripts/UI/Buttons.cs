using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour {

    private GameObject estadoObject;
    private EstadoJuego estadoJuego;

    private void Awake()
    {
        estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
    }

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
        previousScene();
    }

    public void ContinueVariablesOnClick()
    {
        Text coins = GameObject.Find("TextCounterMoney").GetComponent<Text>();
        int numCharacters = (int)GameObject.Find("SliderAnimals").GetComponent<Slider>().value;
        this.estadoJuego.coins = System.Int32.Parse(coins.text);
        this.estadoJuego.numCharacters = numCharacters;
        nextScene();
    }

    public void GoBackCharacterOnClick()
    {
        previousScene();
    }

    public void ContinueCharacterOnClick()
    {
        nextScene();
    }

    public void PlayIndexOnClick()
    {
        nextScene();        
    }

    public void nextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void previousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }

    public void OptionsIndexOnClick()
    {
        //SceneManager.LoadScene("OptionsScene");
    }

    public void HelpIndexOnClick()
    {
        //SceneManager.LoadScene("HelpScene");
    }

    public void ExitIndexOnClick()
    {
        Application.Quit();
    }
}
