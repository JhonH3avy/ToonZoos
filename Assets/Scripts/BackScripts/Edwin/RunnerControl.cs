using UnityEngine;
using System.Collections;

/**
 * Se encarga de ejecutar todos los movimientos de un personaje.
 * Es controlado por un RunnerInput
 * Controla las colisiones con otros jugadores (por cambio de carril)
 * Maneja el caso de eventos ya ocurridos o futuros.
 * */
public class RunnerControl : MonoBehaviour {

	public enum RunnerState {
		Ready, Running, Falling
	};

	public float smooth = .5f;

	// La posición en Y q debe tener el personaje
	public float trackY;

	//Posición que está constantemente moviéndose al frente del personaje,
	//Sirve para sincronizar posiciones suavemente
	private float nextX;

	private RunnerState nextState; 
	private RunnerState curState; 

	private TrackController trackController;

	void Start () {
		trackY = trackController.getAvailableTrack();

		//FIXME temporalmente para halar el personaje hasta la meta
		Vector3 targetPosition = transform.position + Vector3.right * 10;
		nextX = targetPosition.x;
	}

	/**
	 * Programa el inicio de la acción de correr. 
	 * Si el tiempo es en el pasado (delta negativo), predecir para conocer la posición actual 
	 * */
	void Run(Vector3 from, float deltaTime){
		//TODO considerar el caso del pasado

	}

	/**
	 * Programa el inicio de la acción de saltar. 
	 * Si el tiempo es en el pasado (delta negativo), predecir para conocer la posición actual 
	 * */
	void Jump(Vector3 here, float atTime){
		//TODO  considerar el caso del pasado	
	}

	/**
	 * Programa el inicio de la acción de caer. 
	 * Si el tiempo es en el pasado (delta negativo), predecir para conocer la posición actual 
	 * */
	void Fall(Vector3 here, float atTime){
		//TODO  considerar el caso del pasado
	}

	/**
	 * Programa el inicio de la acción de cambiar al carril superior. 
	 * Si el tiempo es en el pasado (delta negativo), predecir para conocer la posición actual 
	 * */
	void MoveUp(Vector3 here, float atTime){
		//TODO  considerar el caso del pasado
	}

	/**
	 * Programa el inicio de la acción de cambiar al carril inferior.
	 * Si el tiempo es en el pasado (delta negativo), predecir para conocer la posición actual 
	 * */
	void MoveDown(Vector3 here, float atTime){
		//TODO  considerar el caso del pasado
	}

	/**
	 * Aplica todas las acciones programadas para el futuro cercano
	 * */
	void Update () {

		//TODO  basado en transform.position, trackY, nextX; curState, nextState 
		// actualizar el estado para que alcance la posición y el estado dados

		//Perseguir suavemente un punto al frente cambiando de carril si es necesario
		if(transform.position.x != trackY)
			transform.position = new Vector3( transform.position.x, trackY );

		float step = smooth * Time.deltaTime;
		transform.position = Vector3.MoveTowards( transform.position, new Vector3(nextX, trackY), step);
	}
}
