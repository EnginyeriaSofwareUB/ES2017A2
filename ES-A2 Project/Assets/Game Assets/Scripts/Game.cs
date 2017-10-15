using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    [SerializeField] private Text countDownText;
    private Round round;
    private int timeBetweenRounds, timeTurn;  //in seconds
    private TimerGame timerRounds;
    private bool isBetweenRounds;

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
        timeBetweenRounds = 5;
        timeTurn = 10;
        this.startRound();
    }

    void FixedUpdate() {
        if (!this.round.Running && !this.isBetweenRounds) {
            this.endRound();
            this.betweenRounds();
        }
        //Update the countdown
        this.countDownText.text = "Count: " + ((int)round.getTimeLeft()).ToString();
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
