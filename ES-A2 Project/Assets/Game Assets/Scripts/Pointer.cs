using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pointer : MonoBehaviour {

	public static Vector2 Position() {
		return Camera.main.ScreenToWorldPoint(Input.mousePosition);
	}

	/// <summary>
	/// Funcion encargada de calcular angulo entre dos vectores. NOTA: esto estara implementado en CURSOR.cs
	/// </summary>
	/// <param name="vec1"></param>
	/// <param name="vec2"></param>
	/// <returns></returns>
	public static float AngleBetweenVectors(Vector2 vec1, Vector2 vec2) {
		Vector2 dif = vec2 - vec1;
		float angle = Vector2.Angle(Vector2.right, dif);
		float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
		return angle * sign;
	}
}
