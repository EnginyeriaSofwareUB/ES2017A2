using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Character character;
    private bool isGrounded = false;
    private Rigidbody2D rigidbody;

	// Use this for initialization
	void Start () {
        this.character = this.GetComponent<Character>();
        this.rigidbody = this.GetComponent<Rigidbody2D>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        this.movementController();
        
    }

    private void movementController() {
        this.horizontalMovement();
        this.verticalMovement();
        
    }

    private void verticalMovement() {
        float ySpeed = this.character.YSpeed;

        if (Input.GetKeyDown(KeyCode.UpArrow) && this.isGrounded) {
            this.rigidbody.AddForce(new Vector2(0, ySpeed), ForceMode2D.Impulse);
        }
    }

    private void horizontalMovement() {
        float xSpeed = Input.GetAxis("Horizontal") * this.character.XSpeed;
        this.rigidbody.velocity = new Vector2(xSpeed, this.rigidbody.velocity.y);
    }

    void OnCollisionEnter2D(Collision2D other) {
        Debug.Log("Trigger entered");
        if (other.gameObject.tag == "Terrain") {
            this.isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Terrain") {
            this.isGrounded = false;
        }
    }
}
