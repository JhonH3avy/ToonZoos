using UnityEngine;
using System.Collections;

/**
 * Clase encargada de generar los obstáculos para la pista.
 * Puede crear obstáculos de diferente tipo. Para ello usa los prefabs
 * que tiene en el arreglo obstacleType.
 * 
 * */
public class ObstacleFactory : MonoBehaviour {

	//El tipo de obstáculos que puede crear
	public Transform[] obstacleType;

	//El numero de obstáculos por carril
	public int limitPerLane = 3;

	//Semilla para generación pseudoaleatorea
	public int seed = 0;

	/**
	 * Aleatoriamente crear obstáculos con los tipos de obstáculos dados en el momento de preparación de la carrera
	 * */
	void Start () {
		TrackController.PreparingRace += new TrackController.RaceEventHandler(createObstacles);

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	private void createObstacles() 
	{
		print("Generating Obstacles ");
		if(obstacleType != null && obstacleType.Length > 0){
			Random.seed = seed;
			TrackController tc = TrackController.instance;
			// asegurarse que en lo posible todos los 
			for(int l= 0; l < tc.trackCount; l++ ){
				for(int i= 0; i < limitPerLane; i++ ){
					Transform obstacle = obstacleType[(int)(Random.Range(0,obstacleType.Length-.001f))];
					Instantiate( obstacle, tc.GetPointOnLane( l, obstacle.transform.position.y, Random.value ), Quaternion.identity );
				}
			}
		}
	}

}
