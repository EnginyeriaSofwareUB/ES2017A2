using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private int coins;
    [SerializeField] private Character selectedCharacter;
    [SerializeField] private MenuItem selectedProjectile;
    private InventoryController inventory;


    private List<Character> characters;

    public List<Character> Characters {
        get {
            return this.characters;
        }

        set {
            this.characters = value;
        }
    }

    public Character SelectedCharacter {
        get {
            return selectedCharacter;
        }

        set {
            selectedCharacter = value;
        }
    }

    public InventoryController Inventory {
        get {
            return this.inventory;
        }

        set {
            this.inventory = value;
        }
    }

    public MenuItem SelectedProjectile {
        get {
            return this.selectedProjectile;
        }

        set {
            this.selectedProjectile = value;
        }
    }



    // Use this for initialization
    void Awake() {
        this.initVariables();
    }

    public int getCoins() {
        return this.coins;
    }

    public int getNumAliveCharacters() {
        return this.getAliveCharacters().Count;
    }

    // Update is called once per frame
    void Update() {

    }

    public bool hasNoCharacters() {
        return this.getAliveCharacters().Count == 0;
    }

    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.characters = new List<Character>();
        this.characters.AddRange(this.GetComponentsInChildren<Character>());
        this.inventory = this.gameObject.GetComponent<InventoryController>();
    }

    public List<Character> getAliveCharacters() {
        List<Character> aliveCharacters = new List<Character>();
        foreach (Character character in this.Characters) {
            if (character.isAlive()) {
                aliveCharacters.Add(character);
            }
        }
        return aliveCharacters;
    }

    public void openInventory() {
        if (this.selectedProjectile.Ammo <= 0) {
            this.inventory.selectDefaultProjectile();
        }
        this.inventory.openInventory();
    }

    public void closeInventory() {
        this.selectedCharacter.enableCharacter();
        this.inventory.closeInventory();
    }

    public void setSelectedProjectile(MenuItem menuItem) {
        this.SelectedProjectile = menuItem;
    }

    public GameObject useProjectile() {
        if(this.selectedProjectile.Ammo <= 0) {
            this.inventory.selectDefaultProjectile();
        }
        return this.selectedProjectile.useProjectile();
    }
}
