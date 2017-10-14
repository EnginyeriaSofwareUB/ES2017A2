using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour {
    [SerializeField] private int numCharacters;
    [SerializeField] private Sprite monkey;
    private int count; //caracteres seleccionados
    private int orden; //necesario para el orden de seleccion
    [SerializeField] private static int select=0;
    [SerializeField] private bool P1firstPlayer;//true = comienza player 1, false = comienza player 2
    [SerializeField] private List<Character> P1Characters;
    [SerializeField] private List<Character> P2Characters;
    // Use this for initialization
    void Start()
    {
        count = 0;
        orden = 0;
        drawMenu();
    }
	
	// Update is called once per frame
	void Update () {

        if (count < numCharacters * 2) {
            startMenu();
        }
    }

    public static int Select
    {
        get { return select; }
        set { select = value; }
    }

    void startMenu() {
        if (P1firstPlayer) {

            if (select != 0)
            {
                Debug.Log(select);
                if (count % 2 == 0)
                {
                    GameObject t = GameObject.Find("/Circles" + numCharacters + "C/circle " + "(" + orden + ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<SpriteRenderer>().sprite = monkey;
                    P1Characters.Add(new Mole());
                    //select se substituira por character cuando este implementado
                    select = 0;
                    count++;
                    orden++;
                } else if (count % 2 != 0)
                {

                    GameObject t = GameObject.Find("/Circles" + numCharacters + "C/circle " + "(" +(orden+numCharacters-1)+ ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<SpriteRenderer>().sprite = monkey;
                    P2Characters.Add(new Mole());
                    //

                    select = 0;
                    count++;
                }
            }
        }
        else if (!P1firstPlayer)
        {
            if (select != 0)
            {
                if (count % 2 != 0)
                {
                    GameObject t = GameObject.Find("/Circles" + numCharacters + "C/circle " + "(" + orden + ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<SpriteRenderer>().sprite = monkey;
                    P1Characters.Add(new Mole());
                    //

                    select = 0;
                    count++;
                    orden++;
                }
                else if (count % 2 == 0)
                {
                    GameObject t = GameObject.Find("/Circles" + numCharacters + "C/circle " + "(" + (orden + numCharacters) + ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<SpriteRenderer>().sprite = monkey;
                    P2Characters.Add(new Mole());
                    //

                    select = 0;
                    count++;
                }
            }
        }
    }

    void drawMenu() {
        if (numCharacters == 1) {
            GameObject.Find("Circles1C").SetActive(true);
            GameObject.Find("Circles2C").SetActive(false);
            GameObject.Find("Circles3C").SetActive(false);
            GameObject.Find("Circles4C").SetActive(false);
            GameObject.Find("Circles5C").SetActive(false);
            GameObject.Find("Circles6C").SetActive(false);
        }
        else if (numCharacters == 2)
        {
            GameObject.Find("Circles1C").SetActive(false);
            GameObject.Find("Circles2C").SetActive(true);
            GameObject.Find("Circles3C").SetActive(false);
            GameObject.Find("Circles4C").SetActive(false);
            GameObject.Find("Circles5C").SetActive(false);
            GameObject.Find("Circles6C").SetActive(false);
        }
        else if (numCharacters == 3)
        {
            GameObject.Find("Circles1C").SetActive(false);
            GameObject.Find("Circles2C").SetActive(false);
            GameObject.Find("Circles3C").SetActive(true);
            GameObject.Find("Circles4C").SetActive(false);
            GameObject.Find("Circles5C").SetActive(false);
            GameObject.Find("Circles6C").SetActive(false);
        }
        else if (numCharacters == 4)
        {
            GameObject.Find("Circles1C").SetActive(false);
            GameObject.Find("Circles2C").SetActive(false);
            GameObject.Find("Circles3C").SetActive(false);
            GameObject.Find("Circles4C").SetActive(true);
            GameObject.Find("Circles5C").SetActive(false);
            GameObject.Find("Circles6C").SetActive(false);
        }
        else if (numCharacters == 5)
        {
            GameObject.Find("Circles1C").SetActive(false);
            GameObject.Find("Circles2C").SetActive(false);
            GameObject.Find("Circles3C").SetActive(false);
            GameObject.Find("Circles4C").SetActive(false);
            GameObject.Find("Circles5C").SetActive(true);
            GameObject.Find("Circles6C").SetActive(false);
        }
        else if (numCharacters == 6)
        {
            GameObject.Find("Circles1C").SetActive(false);
            GameObject.Find("Circles2C").SetActive(false);
            GameObject.Find("Circles3C").SetActive(false);
            GameObject.Find("Circles4C").SetActive(false);
            GameObject.Find("Circles5C").SetActive(false);
            GameObject.Find("Circles6C").SetActive(true);
        }
    }
}


