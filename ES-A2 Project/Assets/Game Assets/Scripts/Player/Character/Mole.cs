using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mole : Character {

	protected override void Start () {
        base.Start();
        this.disableCharacter();
    }

    protected override void Update () {
        base.Update();
	}
}
