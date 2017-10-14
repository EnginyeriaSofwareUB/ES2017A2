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
		//transform.position = end;

		var vect = end - start;
		double nextX = start.x + vect.x*0.005*speed*currentPos;
		double nextY = start.y + vect.y*0.005*speed*currentPos;
		double nextZ = start.z + vect.z*0.005*speed*currentPos;
		transform.position = new Vector3 ((float) nextX, (float) nextY, (float) nextZ);
		currentPos = currentPos + 1;

		if (start.x < end.x) {
			if (transform.position.x >= end.x) {
				transform.position = start;
				currentPos = 0;
			}
		} else {
			if (transform.position.x <= end.x) {
				transform.position = start;
				currentPos = 0;
			}
		}

	}

	protected void OnCollisionEnter2D(Collision2D other) {
		transform.position = new Vector3(0.0f,0.0f,0.0f);
		if (other.gameObject.tag == "CloudEnd") {
			transform.position = new Vector3(0.0f,0.0f,0.0f);
		}
	}
}
