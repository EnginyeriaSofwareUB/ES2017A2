using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Intangible {
    [Range(0.0F, 10.0F)]
    [SerializeField] private float speed = 1f;
	[SerializeField] private GameObject cloudEnd;
	[SerializeField] private GameObject cloudStart;
    private float speedModifier = 100f;

	// Use this for initialization
	protected void Awake() {
        base.Start();
        this.transform.position = this.cloudStart.transform.position;
	}

    // Update is called once per frame
    override
    protected void Update() {
        base.Update();
        this.transform.position = Vector3.MoveTowards(this.transform.position, this.cloudEnd.transform.position, this.speed / this.speedModifier);
	}

	protected void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "CloudEnd") {
			this.transform.position = this.cloudStart.transform.position;
		}
	}
}
