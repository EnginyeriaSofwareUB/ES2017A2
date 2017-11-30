using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameOptions : MonoBehaviour {
    [SerializeField] private EstadoJuego estadoJuego;
    [SerializeField] private Slider slider2;
    [SerializeField] private Slider slider1;

    // Use this for initialization
    void Start()
    {
        estadoJuego = GameObject.Find("EstadoJuego").GetComponent<EstadoJuego>();
        slider2.value = estadoJuego.volumenEfectos;
        slider1.value = estadoJuego.volumenMusica;
    }

    // Update is called once per frame
    void Update()
    {
        estadoJuego.volumenEfectos = (int)slider2.value;
        estadoJuego.volumenMusica = (int)slider1.value;

    }
}
