using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour {

    protected int health;
    [SerializeField]
    protected int maxhealth;
    [SerializeField]
    protected float xSpeed;
    [SerializeField]
    protected float ySpeed;
    [SerializeField]
    protected float strength;
    [SerializeField]
    private bool fire = false;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject firePoint;
    [SerializeField]
    private Image healthBar;
    [SerializeField]
    protected AudioSource preparetoshootSound;
    [SerializeField]
    protected AudioSource damageRecievedSound;
    [SerializeField]
    protected AudioSource shootSound;
    //private int projDetonationTime;
    [SerializeField, Range(1, 30)]
    private float force;
    [SerializeField, Range(20, 60)]
    private float limitForce = 40f;
    [SerializeField]
    private GameObject forceBar;

    private float initForce;
    private bool startShot;
    private Movement movement;
    private Player player;

    private bool isPlaying = false;

    public int Health {
        get {
            return health;
        }

        set {
            health = value;
        }
    }

    public int MaxHealth {
        get {
            return health;
        }

        set {
            health = value;
        }
    }

    public float XSpeed {
        get {
            return xSpeed;
        }

        set {
            xSpeed = value;
        }
    }

    public float Strength {
        get {
            return strength;
        }

        set {
            strength = value;
        }
    }

    public float YSpeed {
        get {
            return ySpeed;
        }

        set {
            ySpeed = value;
        }
    }

    public bool Fire {
        get {
            return this.fire;
        }

        set {
            this.fire = value;
        }
    }

    protected virtual void Awake() {
        this.damageRecievedSound = Instantiate(this.damageRecievedSound);
        this.preparetoshootSound = Instantiate(this.preparetoshootSound);
        this.shootSound = Instantiate(this.shootSound);
    }

    // Use this for initialization
    protected virtual void Start() {
        this.movement = this.GetComponent<Movement>();
        this.player = this.GetComponentInParent<Player>();
        this.initForce = this.force;
        this.disableCharacter();
        this.healthBar = transform.Find("CharacterCanvas").Find("HealthBG").Find("Health").GetComponent<Image>();
        this.healthBar.canvas.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
    }


    // Update is called once per frame
    protected virtual void Update() {
        if (Time.timeScale != 0) {
            this.MakeShoot();
        }
        if (Input.GetKeyDown(KeyCode.I) && this.isPlaying) {
            this.disableCharacter();
            this.player.openInventory();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider2D) {
        if (collider2D.gameObject.tag == "Projectile") {
            Projectile projectile = collider2D.GetComponent<Projectile>();
            this.Damage(projectile.Damage);
        }
    }

    /**
      * Metodo que deshabilita el personaje (p.e. el movimeinto)
      */
    public void disableCharacter() {
        this.movement.Enabled = false;
        this.arrow.SetActive(false);
        this.startShot = false;
        this.isPlaying = false;
        this.force = this.initForce;
        this.GetComponentInParent<InventoryController>().closeInventory();
    }

    /**
     * Metodo que habilita el personaje (p.e. el movimeinto)
     */
    public void enableCharacter() {
        this.Fire = false;
        this.movement.Enabled = true;
        this.isPlaying = true;
        this.arrow.SetActive(true);
        this.forceBar.SendMessage("Stop");
    }

    /**
     * Comprueba si el personaje sigue vivo
     */
    public bool isAlive() {
        return this.health > 0;
    }

    /// <summary>
    /// Metodo encargado de instanciar el projectil
    /// </summary>
    public void fireProjectile(float angle) {
        this.Fire = true;
        this.GetComponentInParent<Game>().GetComponent<TimerGame>().stop();
        GameObject projectilePrefab = this.player.useProjectile();
        GameObject projectil = Instantiate(projectilePrefab, this.firePoint.transform.position, this.arrow.transform.rotation);
        projectil.transform.parent = this.transform;
        projectil.GetComponent<Projectile>().Angle = angle;
        //projectil.GetComponent<Projectile>().DetonationTime = this.ProjDetonationTime;
        projectil.SetActive(true);
        projectil.SendMessage("Shoot", (this.force));
    }

    /// <summary>
    /// Funcion encargada de rotar la flecha segun vaya el cursor y disparar siempre y cuando tenga el character tenga la flecha
    /// </summary>
    private void MakeShoot() {
        float angle = Pointer.AngleBetweenVectors(this.arrow.transform.position, Pointer.Position());
        if (this.arrow.activeInHierarchy) {
            this.MoveArrow(angle);
            if (Input.GetButtonDown("Fire1")) {
                this.forceBar.SendMessage("Load");
                this.startShot = true;
                int i = Random.Range(0, 10);
                if (i == 4) {
                    preparetoshootSound.Play();
                }
            }
            if (Input.GetButton("Fire1")) {
                float forceFactor = this.forceBar.GetComponent<ForceBar>().GetForce();
                this.force = this.force * (forceFactor + Time.deltaTime);
            }
            if (Input.GetButtonUp("Fire1")) {
                if (this.startShot == true) {
                    this.Shoot(angle);
                }
            }
        }
    }

    /// <summary>
    /// Funcion encargada de quitarle vida al current character
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage) {
        this.health = this.health - damage;
        this.healthBar.fillAmount = (float)this.health / this.maxhealth;
        if (this.health <= 0) {
            this.movement.SetAnimation("morir");
            this.movement.Enabled = false;
            this.arrow.SetActive(false);
            this.startShot = false;
            this.isPlaying = false;
            Destroy(this.gameObject, 1);
        }
        else {
            this.movement.SetAnimation("rebre_mal");
        }

        damageRecievedSound.Play();
        //Debug.Log(this.healthBar.fillAmount);
    }

    /// <summary>
    /// Funcion encargada de poner ammo
    /// </summary>
    /// <param name="ammo"></param>
    public void ApplyAmmo(int ammo) {
        this.player.addAmmoToSelectedProjectile(ammo);
    }

    /// <summary>
    /// Funcion encargada de poner ammo
    /// </summary>
    /// <param name="ammo"></param>
    public void ApplyHealth(int health) {
        this.health = this.health + health;
        if (this.health > this.maxhealth) {
            this.health = this.maxhealth;
        }
    }

    /// <summary>
    /// Funcion encargada de mover el personaje en la direccion que apunta la flecha (puntero del mouse)
    /// </summary>
    /// <param name="angle"></param>
    private void MoveArrow(float angle) {
        if (this.transform.rotation.y == 0.7071068f) {
            MoveArrowRight(angle);
        }
        else if (this.transform.rotation.y == -0.7071068f) {
            MoveArrowLeft(angle);
        }
        else {
            if (this.transform.rotation.y == 0.7066739f) {
                MoveArrowRight(angle);
            }
            else if (this.transform.rotation.y == -0.7066739f) {
                MoveArrowLeft(angle);
            }
        }
    }

    /// <summary>
    /// Funcion encargada de limitar el movimiento de la flecha cuando el jugador mira a la derecha
    /// </summary>
    /// <param name="angle"></param>
    private void MoveArrowRight(float angle) {
        angle = Mathf.Clamp(angle, -80, 80);
        this.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
    }

    /// <summary>
    /// Funcion encargada de limitar el movimiento de la flecha cuando el jugador mira a la izquierda
    /// </summary>
    /// <param name="angle"></param>
    private void MoveArrowLeft(float angle) {
        if (angle > 110) {
            angle = Mathf.Clamp(angle, 100, 180);
            this.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
        else if (angle < -90) {
            angle = Mathf.Clamp(angle, -180, -105);
            this.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));
        }
    }


    /// <summary>
    /// Funcion encargada de gestionar el disparo del proyectil con la animacion determinada
    /// </summary>
    /// <param name="angle"></param>
    private void Shoot(float angle) {
        CheckHandShot();
        if (this.force >= this.limitForce) {
            this.force = this.initForce + this.limitForce;
        }
        this.fireProjectile(angle);
        shootSound.Play();
        this.forceBar.SendMessage("Stop");
        if (this.isAlive()) {
            this.disableCharacter();
        }
    }

    /// <summary>
    /// Funcion encargada poner la animacion de disparar dependiendo de hacia donde mira el character
    /// </summary>
    private void CheckHandShot() {
        if (this.transform.rotation.y == 0.7071068f) {
            this.movement.SetAnimation("disparar_dreta");
        }
        else {
            this.movement.SetAnimation("disparar");
        }
    }

    public Player getPlayer()
    {
        return this.player;
    }
}