using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour {
    public GameObject explosion;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1")) {
            Instantiate(this.explosion, Camera.main.ScreenToWorldPoint(Input.mousePosition), Quaternion.identity);
            explosion.SetActive(true);

        }
    }
}
