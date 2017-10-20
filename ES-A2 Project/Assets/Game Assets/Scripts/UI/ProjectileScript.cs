using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class ProjectileScript : MonoBehaviour {

    public string name;
    public Image background;
    public Image projectileImage;
    public Button button;
    public float damage;
    public float velocity;
    public float weight;
    public int damage_radius;
    public int detonation_time;
    public Text cost;
}
