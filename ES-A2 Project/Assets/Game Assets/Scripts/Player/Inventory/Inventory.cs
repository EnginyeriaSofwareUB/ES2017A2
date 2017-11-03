using System;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private Dictionary<ProjectileScript, int> projectiles;

    // Use this for initialization
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void initInventory()
    {
        this.projectiles = new Dictionary<ProjectileScript, int>();
    }


    /**
     * Metodo para añadir un proyectil concreto y la cantidad
     */
    public void addToInventory(ProjectileScript projectile, int amount)
    {
        if (this.projectiles.ContainsKey(projectile))
            this.projectiles[projectile] += amount;
        else
            this.projectiles.Add(projectile, amount);
    }


    /**
     * Metodo para añadir un diccionario de proyectiles con sus respectivas cantidades
     */
    public void addToInventory(Dictionary<ProjectileScript, int> projectiles)
    {
        foreach (KeyValuePair<ProjectileScript, int> ps in this.projectiles)
        {
            this.addToInventory(ps.Key, ps.Value);
        }
    }


    /**
     * Metodo para eliminar un proyectil lanzado (restar uno a la cantidad total)
     * Si no quedan proyectiles de ese tipo lo elimina del inventario.
     */
    public void deleteFiredProjectile(String name)
    {
        foreach (KeyValuePair<ProjectileScript, int> ps in projectiles)
        {
            if (ps.Key.name.Equals(name))
            {
                projectiles[ps.Key]--;
                if (ps.Value <= 0)
                    projectiles.Remove(ps.Key);
            }
        }
    }

    
    /**
     * Metodo que comprueba si esta el proyectil solicitado.
     * Si esta devuelve un subdiccionario con el proyectil y la cantaidad.
     * Si no devuelve null
     */
    public Dictionary<ProjectileScript, int> getProjectile(String name)
    {
        Dictionary<ProjectileScript, int> dic = null;
        foreach (KeyValuePair<ProjectileScript, int> ps in projectiles)
        {
            if (ps.Key.name.Equals(name))
            {
                dic = new Dictionary<ProjectileScript, int>();
                dic.Add(ps.Key, ps.Value);
            }
        }
        return dic;
    }

}
