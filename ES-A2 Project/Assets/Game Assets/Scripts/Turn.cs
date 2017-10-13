using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
    private Character character;
    private bool running = true;
    private TimerGame timerGame;
    private int seconds;

    public bool Running {
        get {
            return running;
        }

        set {
            running = value;
        }
    }

    void Start() {
        this.initVariables();
        this.startTurn();
    }

    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.character = this.GetComponent<Round>().getNextCharacter();
        this.seconds = 3;
        this.timerGame = this.gameObject.AddComponent<TimerGame>();
    }

    /**
     * Metodo que inicializa el turno.
     * Habilita el movimiento de los Personajes y inicializa la Courutina del temporizador (provisional)
     */
    public void startTurn() {
        Debug.Log("Turn Start");
        this.character.enableCharacter();
        this.timerGame.init(seconds);
        this.StartCoroutine(this.timer());
    }

    /**
     * Deshabilita el movimiento del personaje y deshabilita el turno
     */
    public void endTurn() {
        Debug.Log("Turn End");
        this.character.disableCharacter();
        this.timerGame.stop();
        Destroy(this.timerGame);
        this.Running = false;
    }

    IEnumerator timer() {
        while (!Input.GetKeyDown(KeyCode.Z) && !this.character.Fire && !this.timerGame.TimeOver)
            yield return null;
        this.endTurn();
    }
}
