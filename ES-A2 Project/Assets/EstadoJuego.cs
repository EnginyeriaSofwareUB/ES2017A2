using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EstadoJuego : MonoBehaviour {

    public static EstadoJuego estadoJuego;
    public int coins;
    public int numCharacters;

    void Awake()
    {
        if(estadoJuego == null)
        {
            estadoJuego = this;
            DontDestroyOnLoad(gameObject);
        } else if(estadoJuego != this)
        {
            Destroy(gameObject);
        }
    }
}
