using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game : MonoBehaviour {
    [SerializeField] private GameObject player1;
    [SerializeField] private GameObject player2;
    private Round round;

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
        this.startRound();
    }

    void FixedUpdate() {
        if (!this.round.Running) {
            this.endRound();
            this.startRound();
        }
    }

    /**
     * Metodo que instancia una Ronda
     */
    private void startRound() {
        this.round = this.gameObject.AddComponent<Round>();
    }

    /**
     * Metodo que destruye la Ronda actual
     */
    private void endRound() {
        Destroy(this.round);
    }
}
