using UnityEngine;
using System.Collections;

public abstract class ObstacleController : MonoBehaviour
{
	private void OnTriggerEnter (Collider c)
	{
		Debug.Log ("Toco");
		if(c.transform.tag == "Player")
		{
			ObstacleEffect (c.gameObject);
		}
	}

	/*
	 * Cada uno de los tipos de obstaculos tendra que sobreescribir el metodo de efecto
	 * que tendra sobre el jugador
	 * */
	protected abstract void ObstacleEffect (GameObject player);
}
