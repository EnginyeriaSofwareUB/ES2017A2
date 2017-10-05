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

    public List<GameObject> getAliveCharacters() {
        List<GameObject> aliveCharacters = new List<GameObject>();
        foreach(GameObject characterGO in this.Characters) {
            if (characterGO.GetComponent<Character>().isAlive()) {
                aliveCharacters.Add(characterGO);
            }
        }
        return aliveCharacters;
    }
}
