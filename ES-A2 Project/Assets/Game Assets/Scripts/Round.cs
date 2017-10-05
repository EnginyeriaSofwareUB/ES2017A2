using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour {
    private Turn turn;
    private List<GameObject> player1Characters;
    private List<GameObject> player2Characters;
    private List<GameObject> characters;

    public List<GameObject> Characters {
        get {
            return characters;
        }

        set {
            characters = value;
        }
    }

    void Start() {
        this.player1Characters = this.GetComponent<Game>().Player1.GetComponent<Player>().Characters;
        this.player2Characters = this.GetComponent<Game>().Player2.GetComponent<Player>().Characters;
        this.Characters = this.getCharacterList();

        this.startRound();
    }

    void FixedUpdate() {
        if (!this.turn.Enabled) {
            Destroy(this.turn);
            this.startTurn();
        }
    }

    public void startRound() {
        Debug.Log("Round Start");
        this.startTurn();
    }

    private void startTurn() {
        this.turn = this.gameObject.AddComponent<Turn>();
    }

    private List<GameObject> getCharacterList() {
        List<GameObject> list = new List<GameObject>();
        list.AddRange(this.player1Characters);
        list.AddRange(this.player2Characters);
        this.shuffle(list);
        return list;
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

    public GameObject getCharacter() {
        GameObject character = this.characters[0];
        this.characters.RemoveAt(0);
        return character;
    }

}
