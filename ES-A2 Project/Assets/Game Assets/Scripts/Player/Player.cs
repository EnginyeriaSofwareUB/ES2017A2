using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {
    [SerializeField] private int coins;
    [SerializeField] private Character selectedCharacter;

    private List<Character> characters;
    private Inventory inventory;
    private InventoryPanel inventoryPanel;

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
    

    // Use this for initialization
    void Awake () {
        this.initVariables();
    }

    public int getCoins()
    {
        return this.coins;
    }

    public int getNumAliveCharacters()
    {
        return this.getAliveCharacters().Count;
    }

    // Update is called once per frame
    void Update () {
        
	}

    public bool hasNoCharacters()
    {
        return this.getAliveCharacters().Count == 0;
    }

    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.characters = new List<Character>();
        this.characters.AddRange(this.GetComponentsInChildren<Character>());
        this.inventory = new Inventory();
        this.inventoryPanel = this.gameObject.AddComponent<InventoryPanel>();
        this.initInventoryPanel();
    }

    public void initInventoryPanel() {
        this.inventoryPanel.gameObject.GetComponent<InventoryPanel>().initBtns(this.inventory.Projectiles);
    }
    public void updateInventoryPanel() {
        this.inventoryPanel.gameObject.GetComponent<InventoryPanel>().updateBtns(this.inventory.Projectiles);
    }

    public void addToInventory(ProjectileInfo p, int amount) {
        this.inventory.addToInventory(p, amount);
    }
    public void addToInventory(Dictionary<ProjectileInfo, int> projectiles) {
        this.inventory.addToInventory(projectiles);
    }
    public void deleteFiredProjectile() {
        this.inventory.deleteFiredProjectile(this.inventoryPanel.gameObject.GetComponent<InventoryPanel>().WepFired);
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



    /**
     * Corutina para comprobar si se ha pulsado un boton del inventario.
     */
    IEnumerator checkBtnWepPressed() {
        while ((this.selectedCharacter == null) || (!this.selectedCharacter.Fire)) {
            yield return null;
        }
        this.inventory.deleteFiredProjectile(this.inventoryPanel.WepFired);
    }

}
