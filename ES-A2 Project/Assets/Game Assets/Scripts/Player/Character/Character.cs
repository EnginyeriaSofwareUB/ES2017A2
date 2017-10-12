using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {

	[SerializeField] protected int health;
	[SerializeField] protected float xSpeed;
	[SerializeField] protected float ySpeed;
	[SerializeField] protected float strength;
	[SerializeField] private bool fire = false;

	[SerializeField] private GameObject prefabCarrot;
	[SerializeField] private GameObject arrow;
	[SerializeField] private GameObject firePoint;

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
	public bool isAlive() {
		return this.health > 0;
	}

	/// <summary>
	/// Metodo encargado de instanciar el projectil
	/// </summary>
	public void fireProjectile(float angle) {
		this.Fire = true;
		GameObject projectil = Instantiate(this.prefabCarrot, this.firePoint.transform.position, this.arrow.transform.rotation);
		projectil.GetComponent<Projectile>().Angle = angle;
		projectil.SetActive(true);
	}

	/// <summary>
	/// Funcion encargada de rotar la flecha segun vaya el cursor y disparar siempre y cuando tenga el character tenga la flecha
	/// </summary>
	private void Shoot() {
		// Rotacion de la flecha       
		float angle = Pointer.AngleBetweenVectors(this.arrow.transform.position, Pointer.Position());
		this.arrow.transform.rotation = Quaternion.Euler(new Vector3(0, 0, angle));

		//TODO ver que deja disparar mas de una vez por turno. Limitarlo.
		if (this.arrow.activeInHierarchy)
		{
			if (Input.GetButtonDown("Fire1"))
			{
				this.fireProjectile(angle);
			}
		}
	}
}
