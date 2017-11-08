using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBox : Tangible {
	[SerializeField] private int ammo;

    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

    void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Character")
        {
            coll.gameObject.SendMessage("ApplyAmmo", 50);
            Destroy(this.gameObject);
        }
    }
}
