using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private List<GameObject> characters;

    public List<GameObject> Characters {
        get {
            return this.characters;
        }

        set {
            this.characters = value;
        }
    }

    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public List<Character> getAliveCharacters() {
        List<Character> aliveCharacters = new List<Character>();
        foreach(GameObject characterGO in this.Characters) {
            Character character = characterGO.GetComponent<Character>();
            if (character.isAlive()) {
                aliveCharacters.Add(character);
            }
        }
        return aliveCharacters;
    }

}
