using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthBox : Tangible {
	[SerializeField] private int health;


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
            coll.gameObject.SendMessage("ApplyHealth", 50);
            Destroy(this.gameObject);
        }
    }
}
