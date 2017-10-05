using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField] protected int health;
    [SerializeField] protected float xSpeed;
    [SerializeField] protected float ySpeed;
    [SerializeField] protected float strength;
    [SerializeField] private bool fire = false;

    public int Health {
        get {
            return health;
        }

        set {
            health = value;
        }
    }

    public float XSpeed {
        get {
            return xSpeed;
        }

        set {
            xSpeed = value;
        }
    }

    public float Strength {
        get {
            return strength;
        }

        set {
            strength = value;
        }
    }

    public float YSpeed {
        get {
            return ySpeed;
        }

        set {
            ySpeed = value;
        }
    }

    public bool Fire {
        get {
            return this.fire;
        }

        set {
            this.fire = value;
        }
    }

    // Use this for initialization
    protected virtual void Start() {
        this.disableCharacter();
    }

    // Update is called once per frame
    protected virtual void Update() {
        if(Input.GetButtonDown("Fire1")) {
            this.fireProjectile();
        }
    }

    public void disableCharacter() {
        Movement movement = this.GetComponent<Movement>();
        movement.Enabled = false;
    }

    public void enableCharacter() {
        Movement movement = this.GetComponent<Movement>();
        this.Fire = false;
        movement.Enabled = true;
    }

    public bool isAlive() {
        return this.health > 0;
    }

    public void fireProjectile() {
        this.Fire = true;
    }
}
