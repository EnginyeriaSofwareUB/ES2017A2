using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CharacterMenu : MonoBehaviour {
    [SerializeField] private int numCharacters;
    [SerializeField] private Texture monkey;
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
    /// <summary>
    /// Funcion encargada de hacer los turnos de seleccion de personaje
    /// </summary>
    /// <returns></returns>
    void startMenu() {
        if (P1firstPlayer) {

            if (select != 0)
            {
                Debug.Log(select);
                if (count % 2 == 0)
                {
                    GameObject t = GameObject.Find("/Canvas/Circles/Circle (" + orden + ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<RawImage>().texture = monkey;
                    P1Characters.Add(new Mole());
                    //select se substituira por character cuando este implementado
                    select = 0;
                    count++;
                    orden++;
                } else if (count % 2 != 0)
                {

                    GameObject t = GameObject.Find("/Canvas/Circles/Circle (" + (orden+numCharacters-1) + ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<RawImage>().texture = monkey;
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
                    GameObject t = GameObject.Find("/Canvas/Circles/Circle (" + orden + ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<RawImage>().texture = monkey;
                    P1Characters.Add(new Mole());
                    //

                    select = 0;
                    count++;
                    orden++;
                }
                else if (count % 2 == 0)
                {
                    GameObject t = GameObject.Find("/Canvas/Circles/Circle (" + (orden+numCharacters) + ")");
                    //Como solo hay un caracater no se mira que tipo ha seleccionado
                    t.GetComponent<RawImage>().texture = monkey;
                    P2Characters.Add(new Mole());
                    //

                    select = 0;
                    count++;
                }
            }
        }
    }


    /// <summary>
    /// Funcion encargada de dibujar los circulos del menu (dependiendo de cuantos personajes hay)
    /// </summary>
    /// <returns></returns>
    void drawMenu() {
        if (numCharacters == 1) {
            GameObject.Find("/Canvas/Circles/Circle (1)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (2)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (3)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (4)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (5)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (7)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (8)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (9)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (10)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (11)").SetActive(false);
        }
        else if (numCharacters == 2)
        {
            GameObject.Find("/Canvas/Circles/Circle (2)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (3)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (4)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (5)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (8)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (9)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (10)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (11)").SetActive(false);
        }
        else if (numCharacters == 3)
        {
            GameObject.Find("/Canvas/Circles/Circle (3)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (4)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (5)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (9)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (10)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (11)").SetActive(false);
        }
        else if (numCharacters == 4)
        {
            GameObject.Find("/Canvas/Circles/Circle (4)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (5)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (10)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (11)").SetActive(false);
        }
        else if (numCharacters == 5)
        {
            GameObject.Find("/Canvas/Circles/Circle (5)").SetActive(false);
            GameObject.Find("/Canvas/Circles/Circle (11)").SetActive(false);
        }
    }
}


