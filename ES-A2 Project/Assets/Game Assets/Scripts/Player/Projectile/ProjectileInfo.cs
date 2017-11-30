using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProjectileInfo {
    public GameObject projectile;
    public int ammo;

    public ProjectileInfo(GameObject projectile, int ammo) {
        this.projectile = projectile;
        this.ammo = ammo;
    }
}
