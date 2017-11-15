using System;
using System.Collections.Generic;

public class Inventory
{
    private Dictionary<ProjectileInfo, int> projectiles;

    public Inventory()
    {
        this.projectiles = new Dictionary<ProjectileInfo, int>();
    }


    public Dictionary<ProjectileInfo, int> Projectiles
    {
        get
        {
            return new Dictionary<ProjectileInfo, int>(this.projectiles);
        }
    }


    /**
     * Metodo para añadir un proyectil concreto y la cantidad
     */
    public void addToInventory(ProjectileInfo projectile, int amount)
    {
        if (this.projectiles.ContainsKey(projectile))
            this.projectiles[projectile] += amount;
        else
            this.projectiles.Add(projectile, amount);
    }


    /**
     * Metodo para añadir un diccionario de proyectiles con sus respectivas cantidades
     */
    public void addToInventory(Dictionary<ProjectileInfo, int> projectiles)
    {
        foreach (KeyValuePair<ProjectileInfo, int> ps in this.projectiles)
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
        foreach (KeyValuePair<ProjectileInfo, int> ps in projectiles)
        {
            if (ps.Key.projectileName.StartsWith(name))
            {
                projectiles[ps.Key]--;
                if (ps.Value <= 0)
                    projectiles.Remove(ps.Key);
                break;
            }
        }
    }


    /**
     * Metodo que comprueba si esta el proyectil solicitado.
     * Si esta devuelve un subdiccionario con el proyectil y la cantaidad.
     * Si no devuelve null
     */
    public Dictionary<ProjectileInfo, int> getProjectile(String name)
    {
        Dictionary<ProjectileInfo, int> dic = null;
        foreach (KeyValuePair<ProjectileInfo, int> ps in projectiles)
        {
            if (ps.Key.projectileName.Equals(name))
            {
                dic = new Dictionary<ProjectileInfo, int>();
                dic.Add(ps.Key, ps.Value);
                break;
            }
        }
        return dic;
    }


    /**
     * Metodo para poder 'imprimir' el inventario como un diccionario de 'nombre: unidades'
     */
    public String dicToString()
    {
        String s = "";
        foreach (KeyValuePair<ProjectileInfo, int> ps in projectiles)
            s += ps.Key.projectileName + ": " + ps.Value + ",\n\r";
        return s;
    }

}
