using UnityEngine;
using System.Collections;

/**
 * Asigna carriles a los jugadores.
 * Entrega la posición.Y del carril arriba o abajo de uno dado
 * Dispara el inicio de la carrera.
 * Detecta llegada a la meta y el ganador.
 * */
public class TrackController : MonoBehaviour {

	//Numero de carriles
	public int trackCount;

	//Collider para detectar la llegada a la meta
	private Collider2D collider;

	// Use this for initialization
	void Start () {
		collider = GetComponent<Collider2D>();
	}
	
	// Update is called once per frame
	void Update () {
	//Pintar un ray de cada carril para depuracion
	}

	//FIXME crear evento de inicio de carrera
	//FIXME crear evento de fin de carrera

	/**
	 * Retorna la coordenada Y de una pista disponible o negativo si no hay ninguna disponible 
	 * */
	public float getAvailableTrack(){
		//TODO Implementar
		return 0f;
	}

	/**
	 * Retorna la coordenada Y de la pista que está por encima de track.
	 * */
	float GetTrackAbove(float track){
		//TODO basado en la altura del collider, retornar el track.Y que queda justo encima 
		//	de "track" dado el número de carriles. Si supera el borde, retorna el borde
		return track;
	}

	float GetTrackBelow(float track){
		//TODO basado en la altura del collider, retornar el track.Y que queda justo encima 
		//	de "track" dado el número de carriles. Si supera el borde, retorna el borde
		return track;	
	}
}
