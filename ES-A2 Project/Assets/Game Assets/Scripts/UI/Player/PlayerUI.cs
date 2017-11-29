using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerUI {
    private int coins = 1000;
    private int charactersCount = 3;
    [SerializeField]
    private List<GameObject> characters = new List<GameObject>();
    [SerializeField]
    private Dictionary<GameObject, int> projectiles = new Dictionary<GameObject, int>();
    private int aliveCharacters;

    public int Coins {
        get {
            return coins;
        }

        set {
            coins = value;
        }
    }

    public int CharactersCount {
        get {
            return charactersCount;
        }

        set {
            charactersCount = value;
        }
    }

    public List<GameObject> Characters {
        get {
            return characters;
        }

        set {
            characters = value;
        }
    }

    public Dictionary<GameObject, int> Projectiles {
        get {
            return projectiles;
        }

        set {
            projectiles = value;
        }
    }

    public int AliveCharacters {
        get {
            return this.aliveCharacters;
        }

        set {
            this.aliveCharacters = value;
        }
    }

    public void initProjectiles(GameObject defaultProjectile) {
        this.projectiles = new Dictionary<GameObject, int>();
        this.projectiles.Add(defaultProjectile, 99);
    }
}
