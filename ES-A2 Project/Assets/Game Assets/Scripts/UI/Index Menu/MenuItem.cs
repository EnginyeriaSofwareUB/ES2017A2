using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuItem : MonoBehaviour {

    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private int ammo;
    [SerializeField] private Text textBoxAmmo;
    private Toggle toggleProjectile;

    public int Ammo {
        get {
            return this.ammo;
        }

        set {
            this.ammo = value;
        }
    }

    public GameObject ProjectilePrefab {
        get {
            return this.projectilePrefab;
        }

        set {
            this.projectilePrefab = value;
        }
    }

    // Use this for initialization
    void Awake () {
        this.toggleProjectile = this.GetComponent<Toggle>();
	}
	
	// Update is called once per frame
	void Update () {
		if(this.Ammo != int.Parse(this.textBoxAmmo.text)) {
            this.textBoxAmmo.text = this.Ammo.ToString();
        }
        if(this.ammo <= 0) {
            this.toggleProjectile.isOn = false;
            this.toggleProjectile.interactable = false;
        }
    }

    public GameObject useProjectile() {
        if (this.ammo < 99)
            this.ammo--;
        return this.projectilePrefab;
    }
}
