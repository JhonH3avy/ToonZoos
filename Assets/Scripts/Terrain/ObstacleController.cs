using UnityEngine;
using System.Collections;

public abstract class ObstacleController : MonoBehaviour
{
	private void Awake ()
	{
	}

	private void Start ()
	{
		//TODO El track controller me tiene que pasar el x para ubicar al obstaculo
		//transform.position.Set (transform.position.x, transform.position.y, tracker.GetLaneByIdx (Random.Range(0,4)));
	}

	private void OnTriggerEnter (Collider c)
	{
		if(c.transform.tag.Contains("Player"))
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
