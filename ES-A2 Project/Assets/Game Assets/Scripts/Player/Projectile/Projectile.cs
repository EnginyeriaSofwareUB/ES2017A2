using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

    [SerializeField] protected AudioSource projectileImpact;
    [SerializeField] protected float weight;
    [SerializeField] protected float speed;
    [SerializeField] protected int damage;
    [SerializeField] protected int detonationTime;
    [SerializeField] protected CircleCollider2D afectationArea;
    [SerializeField] protected int ammo;
    [SerializeField] protected GameObject explosionPrefab;
    [SerializeField] protected List<string> colliderDestroy;

    Rigidbody2D rb2;
    private float angle;
    private TimerGame timerProjectile;

    public float Angle {
        get {
            return angle;
        }

        set {
            angle = value;
        }
    }


    public float Weight {
        get {
            return weight;
        }

        set {
            weight = value;
        }
    }

    public float Speed {
        get {
            return speed;
        }

        set {
            speed = value;
        }
    }

    public int Damage {
        get {
            return damage;
        }

        set {
            damage = value;
        }
    }

    public int DetonationTime {
        get {
            return detonationTime;
        }

        set {
            detonationTime = value;
        }
    }

    public CircleCollider2D AfectationArea {
        get {
            return afectationArea;
        }

        set {
            afectationArea = value;
        }
    }

    public int Ammo {
        get {
            return ammo;
        }

        set {
            ammo = value;
        }
    }

    /// <summary>
    /// Funcion encargada de disparar el projectile con la fuerza que tiene el personaje
    /// </summary>
    /// <param name="force"></param>
    protected virtual void Shoot(float force) {
        this.rb2 = GetComponent<Rigidbody2D>();
        this.rb2.mass = this.weight;
        this.rb2.AddForce(transform.right * (this.speed+force), ForceMode2D.Impulse);
        this.timerProjectile = this.gameObject.AddComponent<TimerGame>();
        this.DetonationTime = 6;
        this.timerProjectile.init(this.DetonationTime);
    }

    // Update is called once per frame
    protected virtual void Update() {
        Vector2 velocity = this.rb2.velocity;
        this.angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(this.angle, Vector3.forward);
        //Debug.Log("getTimeLeft(): " + this.timerProjectile.getTimeLeft());
        if (this.timerProjectile.TimeOver) {
            this.timerProjectile.stop();
            Destroy(this.timerProjectile);
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Funcion que permite destruir el objeto (projectil) una vez haga colision con el Collider2D correspondiente
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision) {
        if (this.colliderDestroy.Contains(collision.gameObject.tag)) {
            projectileImpact.Play();
            SubtractLife(collision);
            this.timerProjectile.stop();
            Destroy(this.timerProjectile);
            this.explode();
            Destroy(gameObject);
        }
    }

    /// <summary>
    /// Funcion encargada de quitarle vida al character que le de el proyectil
    /// </summary>
    /// <param name="collision"></param>
    private void SubtractLife(Collider2D collision) {
        if (collision.gameObject.tag == "Character") {
            collision.GetComponent<Character>().Damage(this.damage);
        }
    }

    // Funcion a activar: Si sale de la Escena el projectil tenemos que eliminarlo tambien para liberar memoria
    /* void OnBecameInvisible(){
        Destroy(gameObject)
     }*/

    /// <summary>
    /// Funcion encargada de anadir ammo
    /// </summary>
    /// <param name="ammo"></param>
    protected void AddAmmo(int ammo)
    {
        this.ammo = this.ammo + ammo;
    }

    private void explode() {
        GameObject explosion = Instantiate(this.explosionPrefab);
        explosion.transform.position = this.transform.position;
        explosion.SetActive(true);
    }
}