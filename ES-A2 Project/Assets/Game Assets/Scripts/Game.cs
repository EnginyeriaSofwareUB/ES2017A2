using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Game : MonoBehaviour {
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Text countDownText;
    [SerializeField] private int timeBetweenRounds = 5;
    [SerializeField] private int timeTurn = 10;

    private Round round;
    private TimerGame timerRounds;
    private bool isBetweenRounds;
    private EstadoJuego estadoJuego;

    public GameObject Player1 {
        get {
            return this.player1;
        }

        set {
            this.player1 = value;
        }
    }

    public GameObject Player2 {
        get {
            return this.player2;
        }

        set {
            this.player2 = value;
        }
    }

    // Use this for initialization
    void Start () {
        this.estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        this.initPlayers();
        this.startRound();
    }

    void FixedUpdate() {
        if (!this.round.Running && !this.isBetweenRounds) {
            this.endRound();
            this.betweenRounds();
        }
        //Update the countdown
        this.countDownText.text = "Count: " + round.getTimeLeft();
    }

    /**
     * Introduce los proyectiles seleccionados en los inventarios de los jugadores
     */
    private void initPlayers() {
        this.player1.GetComponent<Player>().addToInventory(this.estadoJuego.player1_projectiles);
        this.player2.GetComponent<Player>().addToInventory(this.estadoJuego.player2_projectiles);
    }

    /**
     * Metodo que instancia una Ronda
     */
    private void startRound() {
        this.round = this.gameObject.AddComponent<Round>();
        this.round.TimeTurn = this.timeTurn;
        this.isBetweenRounds = false;
    }

    /**
     * Metodo que destruye la Ronda actual
     */
    private void endRound() {
        Destroy(this.round);
    }

    /**
     * Metodo con el timer entre rondas
     */
    private void betweenRounds() {
        this.isBetweenRounds = true;
        this.timerRounds = this.gameObject.AddComponent<TimerGame>();
        this.timerRounds.init(this.timeBetweenRounds);
        this.StartCoroutine(this.initTimerRound());
    }

    /**
     * Timer que se ejecuta entre rondas
     */
    IEnumerator initTimerRound() {
        while (!this.timerRounds.TimeOver) {
            yield return null;
        }
        this.timerRounds.stop();
        Destroy(this.timerRounds);
        this.startRound();
    }
}
