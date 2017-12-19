using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
    private Character character;
    private bool running = true;
    private TimerGame timerGame;
    private int seconds;
    private bool projectileDestroyed = false;

    public bool Running {
        get {
            return running;
        }

        set {
            running = value;
        }
    }

    public int Time {
        get {
            return seconds;
        }
        set {
            seconds = value;
        }
    }

    void Start() {
        this.initVariables();
        this.startTurn();
    }

    public Character Character
    {
        get
        {
            return this.character;
        }
    }
    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.character = this.GetComponent<Round>().getNextCharacter();        
        this.timerGame = this.gameObject.AddComponent<TimerGame>();   
    }

    /**
     * Metodo que inicializa el turno.
     * Habilita el movimiento de los Personajes y inicializa la Courutina del temporizador (provisional)
     */
    public void startTurn() {
        Debug.Log("Turn Start");
        this.character.enableCharacter();
        this.timerGame.init(this.seconds);
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
        while (!this.timerGame.TimeOver && !this.ProjectileDestroyed)
            yield return null;
        yield return new WaitForSeconds(1);
        this.endTurn();
    }

    /**
     * Retorna el timepo que queda en ese turno
     */
    public double getTimeLeft() {
        double value = timerGame.getTimeLeft();
        value = value + 1;
        if (value > this.seconds) {
            return this.seconds;
        }
        else {
            return value;
        }

    }

    public bool ProjectileDestroyed {
        get {
            return projectileDestroyed;
        }
        set {
            projectileDestroyed = value;
        }
    }

}
