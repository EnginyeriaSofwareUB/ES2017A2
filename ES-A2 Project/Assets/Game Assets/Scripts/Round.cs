using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Round : MonoBehaviour {
    private Turn turn;
    private List<GameObject> player1Characters;
    private List<GameObject> player2Characters;
    private List<GameObject> characters;

    void Start() {
        this.player1Characters = this.GetComponent<Game>().Player1.GetComponent<Player>().Characters;
        this.player2Characters = this.GetComponent<Game>().Player2.GetComponent<Player>().Characters;
        this.characters = this.getCharacterList();

        this.startRound();
    }

    public void startRound() {
        Debug.Log("Round Start");
        foreach (GameObject character in this.characters){
            this.startTurn(character);
        }
    }

    private void startTurn(GameObject character) {
        this.turn = new Turn(character);
        this.turn.startTurn();
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

}
