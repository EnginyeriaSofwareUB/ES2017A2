using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour
{

    [SerializeField] protected float weight;
    [SerializeField] protected float speed;
    [SerializeField] protected float damage;
    [SerializeField] protected int detonationArea;
    [SerializeField] protected int ammo;

    Rigidbody2D rb2;
    private float angles;

    // setters i getters

    public float Angles
    {
        get
        {
            return angles;
        }

        set
        {
            angles = value;
        }
    }


    public float Weight
    {
        get
        {
            return weight;
        }

        set
        {
            weight = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    public float Damage
    {
        get
        {
            return damage;
        }

        set
        {
            damage = value;
        }
    }

    public int DetonationArea
    {
        get
        {
            return detonationArea;
        }

        set
        {
            detonationArea = value;
        }
    }

    public int Ammo
    {
        get
        {
            return ammo;
        }

        set
        {
            ammo = value;
        }
    }



    // Use this for initialization
    protected virtual void Start()
    {
        /* Se le da fuerza al projectil para que salga disparado.
        Esto se modificara para que la fuerza sea a partir de lo que mantengas pulsado el raton
        con una barra (la flecha misma) como referencia para saber si sera con mucha o poca fuerza */
        rb2 = GetComponent<Rigidbody2D>();
        rb2.mass = weight; // Aplicamos el peso a la masa del RigidBody2D para que despues distingamos entre una manzana o sandria por ej.
        rb2.AddForce(transform.right * speed);
    }

    // Update is called once per frame
    protected virtual void Update()
    {
        // Hacer que Artefacto rote al caer. Asi se ve real el disparo con su parabola
        Vector2 velocity = rb2.velocity;
        angles = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angles, Vector3.forward);

    }

    /// <summary>
    /// Funcion que permite destruir el objeto (projectil) una vez haga colision con el Collider2D correspondiente
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "DestroyProjectil")
        {
            Destroy(gameObject);
        }
    }


    // Funcion a implementar: Si sale de la Escena el projectil tenemos que eliminarlo tambien

}
