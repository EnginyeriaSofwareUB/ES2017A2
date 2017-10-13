using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Projectile : MonoBehaviour {

	[SerializeField] protected float weight;
	[SerializeField] protected float speed;
	[SerializeField] protected float damage;
	[SerializeField] protected int detonationArea;
	[SerializeField] protected int ammo;
	[SerializeField] protected List<string> colliderDestroy;

	Rigidbody2D rb2;
	private float angle;

	public float Angle
	{
		get
		{
			return angle;
		}

		set
		{
			angle = value;
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
		this.rb2 = GetComponent<Rigidbody2D>();
		this.rb2.mass = this.weight;
		this.rb2.AddForce(transform.right * this.speed, ForceMode2D.Impulse);
	}

	// Update is called once per frame
	protected virtual void Update() {
		Vector2 velocity = this.rb2.velocity;
		this.angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.AngleAxis(this.angle, Vector3.forward);
	}

	/// <summary>
	/// Funcion que permite destruir el objeto (projectil) una vez haga colision con el Collider2D correspondiente
	/// </summary>
	/// <param name="collision"></param>
	private void OnTriggerEnter2D(Collider2D collision) {
		if ( this.colliderDestroy.Contains(collision.gameObject.tag))
		{
			Destroy(gameObject);
		}
	}

	// Funcion a implementar: Si sale de la Escena el projectil tenemos que eliminarlo tambien

}