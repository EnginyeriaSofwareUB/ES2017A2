using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn {
    private GameObject character;

    public Turn(GameObject character) {
        this.character = character;
    }

    public void startTurn() {
        Debug.Log("Turn Start");
        this.character.GetComponent<Character>().enableCharacter();

        this.endTurn();
    }

    public void endTurn() {
        Debug.Log("Turn End");
        this.character.GetComponent<Character>().disableCharacter();
    }
}
