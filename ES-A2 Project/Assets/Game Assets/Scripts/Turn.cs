using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
    private Character character;
    private bool running = true;

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
    }

    /**
     * Metodo que inicializa el turno.
     * Habilita el movimiento de los Personajes y inicializa la Courutina del temporizador (provisional)
     */
    public void startTurn() {
        Debug.Log("Turn Start");
        this.character.enableCharacter();
        this.StartCoroutine(this.timer());
    }

    /**
     * Deshabilita el movimiento del personaje y deshabilita el turno
     */
    public void endTurn() {
        Debug.Log("Turn End");
        this.character.disableCharacter();
        this.Running = false;
    }

    IEnumerator timer() {
        while (!Input.GetKeyDown(KeyCode.Z) && !this.character.Fire)
            yield return null;
        this.endTurn();
    }
}
