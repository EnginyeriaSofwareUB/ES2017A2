using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Intangible {

	private float currentPos = 0;
	[SerializeField] private float speed = 1f;
	[SerializeField] private GameObject cloudEnd;
	[SerializeField] private GameObject cloudStart;

	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {
		var start = cloudStart.transform.position;
		var end = cloudEnd.transform.position;

        currentPos = currentPos + 0.0001f * speed;
        transform.position = Vector3.Lerp (start, end, currentPos);
		
	}

	protected void OnTriggerEnter2D(Collider2D other){
		if (other.gameObject.tag == "CloudEnd") {
			this.currentPos = 0;
		}
	}
}
