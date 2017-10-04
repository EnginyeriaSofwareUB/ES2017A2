using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField] protected int health;
    [SerializeField] protected float xSpeed;
    [SerializeField] protected float ySpeed;
    [SerializeField] protected float strength;

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

    // Use this for initialization
    protected virtual void Start() {
        
    }

    // Update is called once per frame
    protected virtual void Update() {
    }

    public void disableCharacter() {
        Movement movement = this.GetComponent<Movement>();
        movement.Enabled = false;
    }

    public void enableCharacter() {
        Movement movement = this.GetComponent<Movement>();
        movement.Enabled = true;
    }

    public bool isAlive() {
        return this.health > 0;
    }
}
