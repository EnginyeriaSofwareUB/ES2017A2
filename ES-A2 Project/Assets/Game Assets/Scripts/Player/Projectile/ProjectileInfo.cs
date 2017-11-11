using System.Collections.Generic;
using UnityEngine;

public class ProjectileInfo {

    public string projectileName;
    public float weight;
    public float speed;
    public int damage;
    public int detonationTime;
    public CircleCollider2D afectationArea;
    public int ammo;
    public List<string> colliderDestroy;
}
