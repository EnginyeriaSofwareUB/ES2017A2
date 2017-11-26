using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMenu : MonoBehaviour {
    private int numCharacters;
    [SerializeField] private Texture monkey;
    //[SerializeField] private static int select=0;
    private bool P1firstPlayer;//true = comienza player 1, false = comienza player 2

    private EstadoJuego estadoJuego;

    private int contador; //caracteres seleccionados
    private int añadidos; //necesario para el orden de seleccion

    [SerializeField] private Text player1_money;
    [SerializeField] private Text player2_money;

    [SerializeField] private user_active users_active;


    private void Awake()
    {
        estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        player1_money.text = estadoJuego.coins.ToString();
        player2_money.text = estadoJuego.coins.ToString();
        numCharacters = estadoJuego.numCharacters;
        contador = numCharacters * 2;
        añadidos = 0;
    }

    // Use this for initialization
    void Start()
    {
        drawMenu();
        P1firstPlayer = true;
    }

    private void Update()
    {
        if (P1firstPlayer)
        {
            users_active.player1.enabled = false;
            users_active.player1active.enabled = true;
            users_active.player2.enabled = true;
            users_active.player2active.enabled = false;
        }
        else
        {
            users_active.player1.enabled = true;
            users_active.player1active.enabled = false;
            users_active.player2.enabled = false;
            users_active.player2active.enabled = true;
        }
    }

    public void onClickCharacter(string nameCharacter)
    {
        if (P1firstPlayer && contador >= 0)
        {
            GameObject t = GameObject.Find("/Canvas/Circles/Circle (" + añadidos + ")");
            //Como solo hay un caracater no se mira que tipo ha seleccionado
            t.GetComponent<RawImage>().texture = monkey;
            estadoJuego.P1Characters.Add(new Mole());
            añadidos += 1;
        }
        else if (!P1firstPlayer && contador >= 0)
        {
            GameObject t = GameObject.Find("/Canvas/Circles/Circle (" + (añadidos + 5) + ")");
            //Como solo hay un caracater no se mira que tipo ha seleccionado
            t.GetComponent<RawImage>().texture = monkey;
            estadoJuego.P2Characters.Add(new Mole());
            //
        }
        contador--;
        P1firstPlayer = !P1firstPlayer;
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


