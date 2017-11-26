﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{
    [SerializeField]
    protected AudioSource preparetoshootSound;
    [SerializeField]
    protected AudioSource damageRecievedSound;
    [SerializeField]
    protected AudioSource shootSound;
    [SerializeField]
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
    private GameObject prefabCarrot;
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject firePoint;
    private Image healthBar;

    //private int projDetonationTime;
    [Range(1, 30)]
    [SerializeField]
    private float force;

    private float initForce;

    [Range(20, 60)]
    [SerializeField]
    private float limitForce = 40f;

    [SerializeField]
    private GameObject forceBar;

    private bool startShot;
    private Movement movement;

    public int Health
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public int MaxHealth
    {
        get
        {
            return health;
        }

        set
        {
            health = value;
        }
    }

    public float XSpeed
    {
        get
        {
            return xSpeed;
        }

        set
        {
            xSpeed = value;
        }
    }

    public float Strength
    {
        get
        {
            return strength;
        }

        set
        {
            strength = value;
        }
    }

    public float YSpeed
    {
        get
        {
            return ySpeed;
        }

        set
        {
            ySpeed = value;
        }
    }

    public bool Fire
    {
        get
        {
            return this.fire;
        }

        set
        {
            this.fire = value;
        }
    }


    // Use this for initialization
    protected virtual void Start()
    {
        this.initForce = this.force;
        this.disableCharacter();
        this.healthBar = transform.Find("CharacterCanvas").Find("HealthBG").Find("Health").GetComponent<Image>();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        if (Time.timeScale != 0)
        {
            this.MakeShoot();
        }
    }

    /**
      * Metodo que deshabilita el personaje (p.e. el movimeinto)
      */
    public void disableCharacter()
    {
        this.movement = this.GetComponent<Movement>();
        this.movement.Enabled = false;
        this.arrow.SetActive(false);
        this.startShot = false;
        this.force = this.initForce;
    }

    /**
     * Metodo que habilita el personaje (p.e. el movimeinto)
     */
    public void enableCharacter()
    {
        this.movement = this.GetComponent<Movement>();
        this.Fire = false;
        this.movement.Enabled = true;
        this.arrow.SetActive(true);
        this.forceBar.SendMessage("Stop");
    }

    /**
     * Comprueba si el personaje sigue vivo
     */
    public bool isAlive()
    {
        return this.health > 0;
    }

    /// <summary>
    /// Metodo encargado de instanciar el projectil
    /// </summary>
    public void fireProjectile(float angle)
    {
        this.GetComponentInParent<Game>().GetComponent<TimerGame>().stop();
        this.Fire = true;
        GameObject projectil = Instantiate(this.prefabCarrot, this.firePoint.transform.position, this.arrow.transform.rotation);
        projectil.transform.parent = this.transform;
        projectil.GetComponent<Projectile>().Angle = angle;
        //projectil.GetComponent<Projectile>().DetonationTime = this.ProjDetonationTime;
        projectil.SetActive(true);
        projectil.SendMessage("Shoot", (this.force));
        this.GetComponentInParent<Player>().deleteFiredProjectile();
        this.GetComponentInParent<Player>().updateInventoryPanel();
    }

    /// <summary>
    /// Funcion encargada de rotar la flecha segun vaya el cursor y disparar siempre y cuando tenga el character tenga la flecha
    /// </summary>
    private void MakeShoot()
    {
        // Rotacion de la flecha       
        float angle = Pointer.AngleBetweenVectors(this.arrow.transform.position, Pointer.Position());
        this.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (this.arrow.activeInHierarchy)
        {
            this.MoveCharacter(angle);
            if (Input.GetButtonDown("Fire1"))
            {
                this.forceBar.SendMessage("Load");
                this.startShot = true;
                int i = Random.Range(0, 10);
                if (i==4) {
                    preparetoshootSound.Play();
                }
                

            }
            if (Input.GetButton("Fire1"))
            {
                float forceFactor = this.forceBar.GetComponent<ForceBar>().GetForce();
                this.force = this.force * (forceFactor + Time.deltaTime);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (this.startShot == true)
                {
                    this.Shoot(angle);                    
                }
            }
        }
    }

    /// <summary>
    /// Funcion encargada de quitarle vida al current character
    /// </summary>
    /// <param name="damage"></param>
    public void Damage(int damage)
    {
        this.health = this.health - damage;
        this.healthBar.fillAmount = (float)this.health / this.maxhealth;
        if (this.health == 0) {
            this.movement.SetAnimation("Shut-right");
            this.movement.Enabled = false;
            this.arrow.SetActive(false);
            this.startShot = false;
            Destroy(this.gameObject, 2);
        }
        damageRecievedSound.Play();
        //Debug.Log(this.healthBar.fillAmount);
    }

    /// <summary>
    /// Funcion encargada de poner ammo
    /// </summary>
    /// <param name="ammo"></param>
    public void ApplyAmmo(int ammo)
    {
        //void pending other tasks
    }

    /// <summary>
    /// Funcion encargada de poner ammo
    /// </summary>
    /// <param name="ammo"></param>
    public void ApplyHealth(int health)
    {
        this.health = this.health + health;
        if (this.health > this.maxhealth)
        {
            this.health = this.maxhealth;
        }
    }

    /// <summary>
    /// Funcion encargada de mover el personaje en la direccion que apunta la flecha (puntero del mouse)
    /// </summary>
    /// <param name="angle"></param>
    private void MoveCharacter(float angle) {
        if (!Input.GetButton("Horizontal")) {
            if ((angle < 90) && (angle > -90)) {
                this.transform.rotation = Quaternion.Euler(0.0f, 90.0f, 0.0f);
            }
            else {
                this.transform.rotation = Quaternion.Euler(0.0f, -90, 0.0f);
            }
        }
        this.healthBar.canvas.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
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
        this.disableCharacter();

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
}