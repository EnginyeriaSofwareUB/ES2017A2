using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turn : MonoBehaviour {
    private GameObject character;
    private bool enabled = true;

    public bool Enabled {
        get {
            return enabled;
        }

        set {
            enabled = value;
        }
    }

    void Start() {
        this.character = this.GetComponent<Round>().getNextCharacter();
        this.startTurn();
    }

    public void startTurn() {
        Debug.Log("Turn Start");
        this.character.GetComponent<Character>().enableCharacter();
        this.StartCoroutine(this.timer());
    }

    public void endTurn() {
        Debug.Log("Turn End");
        this.character.GetComponent<Character>().disableCharacter();
        this.Enabled = false;
    }

    IEnumerator timer() {
        while (!Input.GetKeyDown(KeyCode.Z) && !this.character.GetComponent<Character>().Fire)
            yield return null;
        this.endTurn();
    }
}
