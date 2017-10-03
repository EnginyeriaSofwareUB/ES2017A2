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
        this.initVariables();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        this.movementController();
        
    }

    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.character = this.GetComponent<Character>();
        this.rigidbody = this.GetComponent<Rigidbody2D>();
    }

    /**
     * Controlador de movimientos en todas las direcciones.
     */
    private void movementController() {
        this.horizontalMovement();
        this.verticalMovement();
    }

    /**
     * Controla el movimiento vertical de personaje.
     * Setea isGrounded a false una vez que se haya pulsado el boton.
     */
    private void verticalMovement() {
        float ySpeed = this.character.YSpeed;

        if (Input.GetButtonDown("Jump") && this.isGrounded) {
            this.rigidbody.AddForce(new Vector2(0, ySpeed), ForceMode2D.Impulse);
            this.isGrounded = false;
        }
    }

    /**
     * Controla el movimiento horizontal del personaje.
     */
    private void horizontalMovement() {
        float xSpeed = Input.GetAxis("Horizontal") * this.character.XSpeed;
        this.rigidbody.velocity = new Vector2(xSpeed, this.rigidbody.velocity.y);
    }

    protected void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.tag == "Terrain") {
            this.isGrounded = true;
        }
    }

    protected void OnCollisionStay2D(Collision2D other) {
        if (other.gameObject.tag == "Terrain") {
            this.isGrounded = true;
        }
    }

    protected void OnCollisionExit2D(Collision2D other) {
        if (other.gameObject.tag == "Terrain") {
            this.isGrounded = false;
        }
    }
}
