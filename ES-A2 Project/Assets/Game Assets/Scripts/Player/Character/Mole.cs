using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Character {

	protected override void Start () {
        base.Start();
        this.Health = 100;
        this.MaxHealth = 100;
    }

    protected override void Update () {
        base.Update();
	}
}
