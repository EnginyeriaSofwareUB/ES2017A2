using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cloud : Intangible {

	[SerializeField] private float startX = -6f;
	[SerializeField] private float endX = 6;
	private float currentPos;
	[SerializeField] private float speed = 1f;

	[SerializeField] private float height = 4f;

	// Use this for initialization
	void Start () {		
	}

	// Update is called once per frame
	void Update () {
		currentPos = transform.position.x;

		float nextPos = currentPos + 0.01f * speed;
			
		if (currentPos >= endX) {
			transform.position = new Vector3 (startX, height, transform.position.z);
		} else {
			transform.position = new Vector3 (nextPos, height, transform.position.z);
		}
	}
}
