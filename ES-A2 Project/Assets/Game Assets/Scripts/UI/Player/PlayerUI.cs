using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerUI {
    private int coins;
    private int charactersCount;
    [SerializeField] private List<GameObject> characters = new List<GameObject>();
    [SerializeField] private List<ProjectileInfo> projectiles = new List<ProjectileInfo>();
    [SerializeField] private int aliveCharacters;

    public void initDummyData(GameObject character, GameObject projectile) {
        this.coins = 1000;
        this.characters = new List<GameObject>() {
            character,
            character
        };
        this.charactersCount = this.characters.Count;

        this.initProjectiles(projectile);
    }

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

    public int AliveCharacters {
        get {
            return this.aliveCharacters;
        }

        set {
            this.aliveCharacters = value;
        }
    }

    public List<ProjectileInfo> Projectiles {
        get {
            return this.projectiles;
        }

        set {
            this.projectiles = value;
        }
    }

    public void initProjectiles(GameObject defaultProjectile) {
        this.Projectiles = new List<ProjectileInfo>() {
            new ProjectileInfo(defaultProjectile, 99)
        };
    }
}
