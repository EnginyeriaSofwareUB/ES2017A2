using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour {
    private Turn turn;
    private List<GameObject> player1Characters;
    private List<GameObject> player2Characters;
    private List<GameObject> characters;
    private bool enabled = true;

    public List<GameObject> Characters {
        get {
            return characters;
        }

        set {
            characters = value;
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
        this.Characters = this.getCharacterList();

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
        if (this.characters.Count > 0) {
            this.turn = this.gameObject.AddComponent<Turn>();
        }else {
            this.Enabled = false;
        }
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

    public GameObject getNextCharacter() {
        GameObject character = this.characters[0];
        this.characters.RemoveAt(0);
        return character;
    }

}
