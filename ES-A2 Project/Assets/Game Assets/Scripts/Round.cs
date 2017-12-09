using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour {
    private Turn turn;
    private List<Character> player1Characters;
    private List<Character> player2Characters;
    private Queue<Character> charactersQueue;
    private bool running = true;
    private int timeTurn;

    public Queue<Character> CharactersQueue {
        get {
            return charactersQueue;
        }

        set {
            charactersQueue = value;
        }
    }

    public bool Running {
        get {
            return this.running;
        }

        set {
            this.running = value;
        }
    }

    public int TimeTurn {
        get {
            return this.timeTurn;
        }

        set {
            this.timeTurn = value;
        }
    }

    void Start() {
        this.initVariables();
        Debug.Log("Round Start");
        this.startTurn();
    }

    void FixedUpdate() {
        if (!this.turn.Running && this.Running) {
            this.startTurn();
        }
    }

    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.player1Characters = this.GetComponent<Game>().Player1.GetComponent<Player>().getAliveCharacters();
        this.player2Characters = this.GetComponent<Game>().Player2.GetComponent<Player>().getAliveCharacters();
        this.CharactersQueue = this.getCharacterQueue();
    }

    /**
     * Inicializa el turno. En el caso que la cola de caracteres esta vacia se desabilita la Ronda
     */
    private void startTurn() {
        Destroy(this.turn);
        this.removeNullsFromQueue();
        if (this.charactersQueue.Count > 0) {
            this.turn = this.gameObject.AddComponent<Turn>();
            this.turn.Time = this.TimeTurn;
        }
        else {
            Debug.Log("Round End");
            this.Running = false;
        }
    }

    private bool hasRoundEnded() {
        int count = 0;
        foreach (Character character in this.charactersQueue.ToArray()) {
            if (character != null) {
                count += 1;
            }
        }
        return count > 0;
    }

    /**
     * Construye la cola de caracteres randomizada
     */
    private Queue<Character> getCharacterQueue() {
        List<Character> list = new List<Character>();
        list.AddRange(this.player1Characters);
        list.AddRange(this.player2Characters);
        this.shuffle(list);
        return this.addListToQueue(list);
    }

    /**
     * Añade una lista de objectos a una cola
     */
    private Queue<Character> addListToQueue(List<Character> list) {
        Queue<Character> queue = new Queue<Character>();
        foreach (Character obj in list) {
            queue.Enqueue(obj);
        }
        return queue;
    }

    /**
     * Randomiza una lista de objetos
     */
    private void shuffle(List<Character> list) {
        int size = list.Count;
        for (int i = 0; i < size; i++) {
            Character temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, size);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        for (int i = 0; i < size; i++)
        {
            list[i].Posicion = i;
        }
    }

    /**
     * Devuelve el proximo caracter de la cola
     */
    public Character getNextCharacter() {
        Character character = this.charactersQueue.Dequeue();
        /*while (character == null && this.charactersQueue.Count > 0) {
            character = this.charactersQueue.Dequeue();
        }
        if(character != null) {*/
        character.GetComponentInParent<Player>().SelectedCharacter = character;
        // }    
        return character;
    }

    /**
     * Devuelve el tiempo restante del turno activo
     */
    public int getTimeLeft() {
        int seconds = 0;
        if (this.turn != null) {
            seconds = (int)this.turn.getTimeLeft();
        }

        return seconds;
    }

    public void removeNullsFromQueue() {
        Queue<Character> newQueue = new Queue<Character>();
        while (this.charactersQueue.Count != 0) {
            Character character = this.charactersQueue.Dequeue();
            if (character != null) {
                newQueue.Enqueue(character);
            }
        }
        this.charactersQueue = newQueue;
    }
}
