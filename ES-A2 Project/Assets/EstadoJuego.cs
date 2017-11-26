using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour {

    public static EstadoJuego estadoJuego;
    public int coins;
    public int numCharacters;
    public List<Character> P1Characters;
    public List<Character> P2Characters;
    public Dictionary<ProjectileScript, int> player1_projectiles = new Dictionary<ProjectileScript, int>();
    public Dictionary<ProjectileScript, int> player2_projectiles = new Dictionary<ProjectileScript, int>();

    public int coinsPlayer1;
    public int numCharactersPlayer1;
    public int coinsPlayer2;
    public int numCharactersPlayer2;

    public int volumenEfectos;
    public int volumenMusica;

    void Start()
    {
        if(estadoJuego == null)
        {
            GameObject.DontDestroyOnLoad(this.gameObject);
            estadoJuego = this;
        }
        else
        {
            GameObject.Destroy(this.gameObject);
        }
    }

    public void updateInfo(int coinsPlayer1, int numCharactersPlayer1, int coinsPlayer2, int numCharactersPlayer2)
    {
        this.coinsPlayer1 = coinsPlayer1;
        this.numCharactersPlayer1 = numCharactersPlayer1;
        this.coinsPlayer2 = coinsPlayer2;
        this.numCharactersPlayer2 = numCharactersPlayer2;
    }

    public void resetInfo()
    {
        this.coins = 0;
        this.numCharacters = 0;
        this.P1Characters = new List<Character>();
        this.P2Characters = new List<Character>();
        this.player1_projectiles = new Dictionary<ProjectileScript, int>();
        this.player2_projectiles = new Dictionary<ProjectileScript, int>();

        this.coinsPlayer1 = 0;
        this.numCharactersPlayer1 = 0;
        this.coinsPlayer2 = 0;
        this.numCharactersPlayer2 = 0;
    }
}