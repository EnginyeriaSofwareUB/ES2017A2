using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private int coins;
    [SerializeField] private Character selectedCharacter;

    private List<Character> characters;
    private Inventory inventory;

    public List<Character> Characters {
        get {
            return this.characters;
        }

        set {
            this.characters = value;
        }
    }

    public Character SelectedCharacter
    {
        get
        {
            return selectedCharacter;
        }

        set
        {
            selectedCharacter = value;
        }
    }

    // Use this for initialization
    void Start () {
        this.initVariables();
	}

    // Update is called once per frame
    void Update () {
		
	}

    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.characters = new List<Character>();
        this.characters.AddRange(this.GetComponentsInChildren<Character>());
        this.inventory = this.gameObject.AddComponent<Inventory>();
        this.inventory.initInventory();
    }

    public void addToInventory(ProjectileScript p, int amount) {
        this.inventory.addToInventory(p, amount);
    }
    public void addToInventory(Dictionary<ProjectileScript, int> projectiles) {
        this.inventory.addToInventory(projectiles);
    }

    public List<Character> getAliveCharacters() {
        List<Character> aliveCharacters = new List<Character>();
        foreach(Character character in this.Characters) {
            if (character.isAlive()) {
                aliveCharacters.Add(character);
            }
        }
        return aliveCharacters;
    }
}
