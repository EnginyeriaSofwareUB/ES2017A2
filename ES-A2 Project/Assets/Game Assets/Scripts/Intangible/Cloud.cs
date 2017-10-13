using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Intangible {

	[SerializeField] private float startX = -6f;
	[SerializeField] private float endX = 6;
	private float currentPos = 0;
	[SerializeField] private float speed = 1f;

	[SerializeField] private float height = 4f;

	// Use this for initialization
	void Start () {
	}


	// Update is called once per frame
	void Update () {
		var start = GameObject.Find ("StartPoint").transform.position;
		var end = GameObject.Find ("EndPoint").transform.position;
		var vect = end - start;
		double nextX = start.x + vect.x*0.005*currentPos;
		double nextY = start.y + vect.y*0.005*currentPos;
		double nextZ = start.z + vect.z*0.005*currentPos;
		transform.position = new Vector3 ((float) nextX, (float) nextY, (float) nextZ);
		currentPos = currentPos + 1;

	}

	protected void OnCollisionEnter2D(Collision2D other) {
		if (other.gameObject.tag == "EndPoint") {
			transform.position = new Vector3(0.0f,0.0f,0.0f);
		}
	}
}
