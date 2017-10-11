using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/* Actualmente solo funciona el weight y speed para el proyectil. Se pueden modificar los valores en inspector de unity
pero se pueden asignar mediante base.weight por ej */

public class Carrot : Projectile
{
    // Nota:
    // Se podrian crear un Constructor en ProjectileScript y pasarle por parametro los valores de weight y speed antes de llamar a Start

    // Use this for initialization
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
    }

}
