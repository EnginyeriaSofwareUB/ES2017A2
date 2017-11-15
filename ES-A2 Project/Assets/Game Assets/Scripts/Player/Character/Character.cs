using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class Character : MonoBehaviour
{

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
        this.disableCharacter();
        this.healthBar = transform.Find("CharacterCanvas").Find("HealthBG").Find("Health").GetComponent<Image>();
    }


    // Update is called once per frame
    protected virtual void Update()
    {
        this.Shoot();
    }

    /**
      * Metodo que deshabilita el personaje (p.e. el movimeinto)
      */
    public void disableCharacter()
    {
        Movement movement = this.GetComponent<Movement>();
        movement.Enabled = false;
        this.arrow.SetActive(false);
        this.initForce = this.force;
    }

    /**
     * Metodo que habilita el personaje (p.e. el movimeinto)
     */
    public void enableCharacter()
    {
        Movement movement = this.GetComponent<Movement>();
        this.Fire = false;
        movement.Enabled = true;
        this.arrow.SetActive(true);
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
        this.Fire = true;
        GameObject projectil = Instantiate(this.prefabCarrot, this.firePoint.transform.position, this.arrow.transform.rotation);
        projectil.GetComponent<Projectile>().Angle = angle;
        //projectil.GetComponent<Projectile>().DetonationTime = this.ProjDetonationTime;
        projectil.SetActive(true);
        //this.disableCharacter();
        projectil.SendMessage("Shoot", (this.force));
        this.GetComponentInParent<Player>().deleteFiredProjectile();
        this.GetComponentInParent<Player>().updateInventoryPanel();
        this.force = this.initForce;
    }

    /// <summary>
    /// Funcion encargada de rotar la flecha segun vaya el cursor y disparar siempre y cuando tenga el character tenga la flecha
    /// </summary>
    private void Shoot()
    {
        // Rotacion de la flecha       
        float angle = Pointer.AngleBetweenVectors(this.arrow.transform.position, Pointer.Position());
        this.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

        if (this.arrow.activeInHierarchy)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                this.forceBar.SendMessage("Load");
            }
            if (Input.GetButton("Fire1"))
            {
                float forceFactor = this.forceBar.GetComponent<ForceBar>().GetForce();
                this.force = this.force * (forceFactor + Time.deltaTime);
            }
            if (Input.GetButtonUp("Fire1"))
            {
                if (this.force >= this.limitForce)
                {
                    this.force = this.initForce + this.limitForce;
                }
                this.fireProjectile(angle);
                this.forceBar.SendMessage("Stop");

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
}