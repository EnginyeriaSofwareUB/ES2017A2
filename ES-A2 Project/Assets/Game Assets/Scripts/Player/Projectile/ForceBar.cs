using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceBar : MonoBehaviour
{
    [SerializeField] private GameObject force;
    private Coroutine charge = null;

    /// <summary>
    /// Funcion encargada de comenzar la coroutine
    /// </summary>
    public void Load()
    {
        this.charge = StartCoroutine(Raise());
    }

    /// <summary>
    /// Funcion encargada de parar la coroutine
    /// </summary>
    public void Stop()
    {
        if (this.charge != null)
        {
            StopCoroutine(this.charge);
            this.Restart();
        }

    }

    /// <summary>
    /// Funcion encargada de modificar la escala de la flecha a medida que se mantiene pulsado el raton
    /// </summary>
    /// <returns></returns>
    IEnumerator Raise()
    {
        while (true)
        {
            float size = Mathf.Clamp(this.force.transform.localScale.x + 0.01f, 0f, 1f);
            this.force.transform.localScale = new Vector3(size, 1f, 1f);
            yield return null;
        }
    }

    /// <summary>
    /// Funcion encargada de restablecer los valores de la flecha
    /// </summary>
    public void Restart()
    {
        this.force.transform.localScale = new Vector3(0f, 1f, 1f);
    }

    /// <summary>
    /// Funcion encargada de devolver la fuerza actual (la escala de la flecha)
    /// </summary>
    /// <returns></returns>
    public float GetForce()
    {
        return this.transform.localScale.x;
    }
}