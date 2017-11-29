using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VariablesMenu : MonoBehaviour {
    [SerializeField] private Text coins;
    [SerializeField] private Text characterCount;

    private void Awake() {
        Debug.Log(EstadoJuego.estadoJuego.volumenEfectos);
    }


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
