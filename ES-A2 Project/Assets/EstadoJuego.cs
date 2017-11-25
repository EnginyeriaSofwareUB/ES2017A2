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
}