using System.Collections;
using System.Collections.Generic;
using UnityEngine;

abstract public class Tangible : ObjectItem{
	[SerializeField] protected float weight = 1;
    protected Rigidbody2D rb;

    
    protected override void Start()
    {
        base.Start();
        rb = GameObject.FindObjectOfType<Rigidbody2D>();
        rb.mass = weight;
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }
}
