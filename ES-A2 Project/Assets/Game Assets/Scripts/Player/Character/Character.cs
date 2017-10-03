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
    protected abstract void Start();

    // Update is called once per frame
    protected abstract void Update();


}
