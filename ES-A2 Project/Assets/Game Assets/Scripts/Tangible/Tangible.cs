using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Tangible : Obj {
	[SerializeField] protected float weight = 1;
    protected Rigidbody2D rb;

    public Tangible() {
        rb = GameObject.FindObjectOfType<Rigidbody2D>();
        rb.mass = weight;
    }
}
