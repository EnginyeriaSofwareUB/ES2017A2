using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class winScene : MonoBehaviour {

    [SerializeField] private GameObject player1Lose;
    [SerializeField] private GameObject player1win;
    [SerializeField] private GameObject player2Lose;
    [SerializeField] private GameObject player2win;

    [SerializeField] private GameObject player1LoseCont;
    [SerializeField] private GameObject player1winCont;
    [SerializeField] private GameObject player2LoseCont;
    [SerializeField] private GameObject player2winCont;

    [SerializeField] private GameObject player1thumbup;
    [SerializeField] private GameObject player1thumbdown;
    [SerializeField] private GameObject player2thumbup;
    [SerializeField] private GameObject player2thumbdown;

    [SerializeField] private EstadoJuego estadoJuego;


    void Awake() {
        estadoJuego = EstadoJuego.estadoJuego;

    }

    void Start() {
        if (estadoJuego.player1.AliveCharacters > estadoJuego.player2.AliveCharacters) {
            winPlayer1losePlayer2();
        } else if (estadoJuego.player2.AliveCharacters > estadoJuego.player1.AliveCharacters) {
            winPlayer2losePlayer1();
        } else {
            winPlayer1winPlayer2();
        }
    }

    private void winPlayer2losePlayer1() {
        this.player1Lose.SetActive(true);
        this.player1LoseCont.SetActive(true);
        this.player1win.SetActive(false);
        this.player1winCont.SetActive(false);

        this.player1thumbdown.SetActive(true);
        this.player1thumbup.SetActive(false);

        this.player2Lose.SetActive(false);
        this.player2LoseCont.SetActive(false);
        this.player2win.SetActive(true);
        this.player2winCont.SetActive(true);

        this.player2thumbdown.SetActive(false);
        this.player2thumbup.SetActive(true);
    }

    private void winPlayer1losePlayer2() {
        this.player1Lose.SetActive(false);
        this.player1LoseCont.SetActive(false);
        this.player1win.SetActive(true);
        this.player1winCont.SetActive(true);

        this.player1thumbdown.SetActive(false);
        this.player1thumbup.SetActive(true);

        this.player2Lose.SetActive(true);
        this.player2LoseCont.SetActive(true);
        this.player2win.SetActive(false);
        this.player2winCont.SetActive(false);

        this.player2thumbdown.SetActive(true);
        this.player2thumbup.SetActive(false);
    }

    private void winPlayer1winPlayer2() {
        this.player1Lose.SetActive(false);
        this.player1LoseCont.SetActive(false);
        this.player1win.SetActive(true);
        this.player1winCont.SetActive(true);

        this.player1thumbdown.SetActive(false);
        this.player1thumbup.SetActive(true);

        this.player2Lose.SetActive(false);
        this.player2LoseCont.SetActive(false);
        this.player2win.SetActive(true);
        this.player2winCont.SetActive(true);

        this.player2thumbdown.SetActive(false);
        this.player2thumbup.SetActive(true);
    }
}
