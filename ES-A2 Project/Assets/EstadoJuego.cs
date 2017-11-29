using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour {
    public static EstadoJuego estadoJuego;

    public GameObject defaultProjectile;
    public int volumenEfectos = 5;
    public int volumenMusica = 5;

    public PlayerUI player1 = new PlayerUI();
    public PlayerUI player2 = new PlayerUI();
    
    void Awake() {
        if(EstadoJuego.estadoJuego == null) {
            estadoJuego = this;
            GameObject.DontDestroyOnLoad(this.gameObject);
        }
        this.volumenEfectos = 5;
        this.volumenMusica = 5;
        this.initVariables();
    }

    private void initVariables() {
        this.player1 = new PlayerUI();
        this.player2 = new PlayerUI();
    }

    public void resetInfo() {
        this.initVariables();
    }

    public void setVariablesMenuValues(int coins, int charactersCount) {
        this.player1.Coins = coins;
        this.player1.CharactersCount = charactersCount;
        this.player2.Coins = coins;
        this.player2.CharactersCount = charactersCount;
    }

    public void setCharactersMenuValues(List<GameObject> player1Characters, List<GameObject> player2Characters) {
        this.player1.Characters = player1Characters;
        this.player2.Characters = player2Characters;
    }

    public void setProjectileMenuValues(Dictionary<ProjectileScript, int> projectilesPlayer1, Dictionary<ProjectileScript, int> projectilesPlayer2) {
        this.setProjectiles(this.player1, projectilesPlayer1);
        this.setProjectiles(this.player2, projectilesPlayer2);
    }

    public void setProjectiles(PlayerUI player, Dictionary<ProjectileScript, int> projectiles) {
        player.initProjectiles(this.defaultProjectile);
        foreach (KeyValuePair<ProjectileScript, int> projectile in projectiles) {
            player.Projectiles.Add(projectile.Key.projectile, projectile.Value);
        }
    }

    public void setRemaningCharacters(int charactersPlayer1, int charactersPlayer2) {
        this.player1.AliveCharacters = charactersPlayer1;
        this.player2.AliveCharacters = charactersPlayer2;
    }
}