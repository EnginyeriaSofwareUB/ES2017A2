using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

    [SerializeField] protected int health;
    [SerializeField] protected float xSpeed;
    [SerializeField] protected float ySpeed;
    [SerializeField] protected float strength;
    [SerializeField] private bool fire = false;

    /* Podria ir todo a Player, mañana lo pienso mejor. Hoy subo para que se pueda ver el progreso */
    [SerializeField] private GameObject prefabCarrot;
    [SerializeField] private GameObject arrow;
    private float angles;

    public int Health {
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

    // Use this for initialization
    protected virtual void Start() {
        this.disableCharacter();
    }

    // ---------------------------------------- NUEVO -----------------------------------------


    // Update is called once per frame
    protected virtual void Update()
    {
        this.Shoot();
    }

    /// <summary>
    /// Funcion encargada de rotar la flecha segun vaya el cursor y disparar siempre y cuando tenga el character tenga la flecha
    /// </summary>
    private void Shoot()
    {
        // Rotacion de la flecha
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition); // NOTA: esto estara en CURSOR.cs
        this.angles = this.AngleBetweenVectors(this.transform.position, pos);
        arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angles));

        // si tenemos (activada) la flecha quiere decir que es nuestro turno, asi que podremos disparar
        //TODO ver que deja disparar mas de una vez por turno. Limitarlo.
        if (arrow.activeInHierarchy)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                this.fireProjectile();
            }
        }
    }

    /// <summary>
    /// Funcion encargada de calcular angulo entre dos vectores. NOTA: esto estara implementado en CURSOR.cs
    /// </summary>
    /// <param name="vec1"></param>
    /// <param name="vec2"></param>
    /// <returns></returns>
    private float AngleBetweenVectors(Vector2 vec1, Vector2 vec2)
    {
        Vector2 dif = vec2 - vec1;
        float angle = Vector2.Angle(Vector2.right, dif);
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return angle * sign;
    }


    // ------------------------------------------ FIN NUEVO ------------------------------------------

    /**
      * Metodo que deshabilita el personaje (p.e. el movimeinto)
      */
    public void disableCharacter()
    {
        Movement movement = this.GetComponent<Movement>();
        movement.Enabled = false;
        arrow.SetActive(false);
    }

    /**
     * Metodo que habilita el personaje (p.e. el movimeinto)
     */
    public void enableCharacter()
    {
        Movement movement = this.GetComponent<Movement>();
        this.Fire = false;
        movement.Enabled = true;
        arrow.SetActive(true);

    }

    /**
     * Comprueba si el personaje sigue vivo
     */
    public bool isAlive() {
        return this.health > 0;
    }

    /**
     * (Por Implementar)
     * Intanciar el proyectil
     */
    public void fireProjectile() {
        this.Fire = true;
        GameObject projectil = Instantiate(prefabCarrot, arrow.transform.position, arrow.transform.rotation);
        projectil.GetComponent<Projectile>().Angles = this.angles;
    }
}
