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
}