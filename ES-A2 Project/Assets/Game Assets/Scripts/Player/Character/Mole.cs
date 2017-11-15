using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Character {

    public Mole() {

    }
	protected override void Start () {
        base.Start();
        this.health = 100;
        this.maxhealth = 100;
    }

    protected override void Update () {
        base.Update();
	}
}
