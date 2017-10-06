using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour {
    private Turn turn;
    private List<GameObject> player1Characters;
    private List<GameObject> player2Characters;
    private Queue<GameObject> charactersQueue;
    private bool enabled = true;

    public Queue<GameObject> CharactersQueue {
        get {
            return charactersQueue;
        }

        set {
            charactersQueue = value;
        }
    }

    public bool Enabled {
        get {
            return this.enabled;
        }

        set {
            this.enabled = value;
        }
    }

    void Start() {
        this.player1Characters = this.GetComponent<Game>().Player1.GetComponent<Player>().getAliveCharacters();
        this.player2Characters = this.GetComponent<Game>().Player2.GetComponent<Player>().getAliveCharacters();
        this.CharactersQueue = this.getCharacterQueue ();

        this.startTurn();
    }

    void FixedUpdate() {
        if (!this.turn.Enabled && this.Enabled) {
            this.startTurn();
        }
    }

    private void startTurn() {
        Debug.Log("Round Start");
        Destroy(this.turn);
        if (this.charactersQueue.Count > 0) {
            this.turn = this.gameObject.AddComponent<Turn>();
        }else {
            this.Enabled = false;
        }
    }

    private Queue<GameObject> getCharacterQueue() {
        List<GameObject> list = new List<GameObject>();
        list.AddRange(this.player1Characters);
        list.AddRange(this.player2Characters);
        this.shuffle(list);
        return this.addListToQueue(list);
    }

    private Queue<GameObject> addListToQueue(List<GameObject> list) {
        Queue<GameObject> queue = new Queue<GameObject>();
        foreach(GameObject obj in list) {
            queue.Enqueue(obj);
        }
        return queue;
    }

    private void shuffle(List<GameObject> list) {
        int size = list.Count;
        for (int i = 0; i < size; i++) {
            GameObject temp = list[i];
            int randomIndex = UnityEngine.Random.Range(i, size);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
    }

    public GameObject getNextCharacter() {
        GameObject character = this.charactersQueue.Dequeue();
        return character;
    }
}
