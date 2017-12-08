using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour {
    private Character character;
    private bool isGrounded = false;
    private Rigidbody2D rigidbody;
    [SerializeField] private bool enabled = false;
    [SerializeField] private float fallMultiplier = 2.5f;
    [SerializeField] private float lowJumpMultiplier = 2f;
    Animator animator;
    public bool Enabled {
        get {
            return enabled;
        }

        set {
            enabled = value;
        }
    }

    // Use this for initialization
    void Start() {
        this.initVariables();
    }

    // Update is called once per frame
    void FixedUpdate() {
        this.movementController();
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

    /**
     * Metodo inicializador de variables
     */
    private void initVariables() {
        this.character = this.GetComponent<Character>();
        this.rigidbody = this.GetComponent<Rigidbody2D>();
        this.animator = this.GetComponent<Animator>();
    }

    /**
     * Controlador de movimientos en todas las direcciones.
     */
    private void movementController() {
        if (this.enabled) {
            this.horizontalMovement();
            this.verticalMovement();
        }       
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
            SetAnimation("saltar");
        }
        this.betterJump();
    }

    private void betterJump() {
        if (this.rigidbody.velocity.y < 0) {
            this.rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (this.fallMultiplier - 1) * Time.deltaTime;
        } else if (this.rigidbody.velocity.y > 0 && !Input.GetButton("Jump")) {
            this.rigidbody.velocity += Vector2.up * Physics2D.gravity.y * (this.lowJumpMultiplier - 1) * Time.deltaTime;
        }
    }

    /**
     * Controla el movimiento horizontal del personaje.
     */
    private void horizontalMovement() {
        Vector2 input = new Vector2(Input.GetAxis("Horizontal"), 0);
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero) {
            this.transform.eulerAngles = Vector3.up * Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
        }

        //block axis Z
        Vector3 pos = transform.position;
        pos.z = 0;
        this.transform.position = pos;

        float speed = this.character.XSpeed * inputDir.magnitude;
        this.transform.Translate(this.transform.forward * speed * Time.deltaTime, Space.World);

        if (Input.GetButton("Horizontal")) {
            SetAnimation("Caminar");
        }
    }

    /// <summary>
    /// Funcion encargada de poner la animacion que le llega por parametro
    /// </summary>
    /// <param name="s"></param>
    public void SetAnimation(String s) {
        this.animator.Play(s);
    }
}
