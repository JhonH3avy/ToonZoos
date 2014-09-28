using UnityEngine;
using System.Collections;

/**
 * Se encarga de controlar los eventos y las características propias de la pista d carreras.
 * 
 * Asigna carriles 
 * Da la información de que carril queda arriba y abajo de un carril dado
 * 
 * */
public class TrackController2 : MonoBehaviour {

	public float trackLength = 20;
	public int trackCount = 4;

	private float startPos = 0;

	public delegate void RaceEventHandler ();
	public static event RaceEventHandler RaceStarting;

	// Use this for initialization
	void Start () {
		trackCount = Mathf.Max(1, Mathf.Min(4, trackCount));
		trackLength = Mathf.Abs(trackLength);
		startPos = transform.position.x;
		transform.position += Vector3.right * trackLength;
	}

	public float GetFinishLineX(){
		return transform.position.x;
	}

	public float GetStartLineX(){
		return startPos;
	}

	/**
	 * Retorna el numero del carril empezando en cero
	 * */
	public int GetLaneIdx(float curLane){
		return (int)(curLane * trackCount / transform.localScale.z);
	}

	public float GetLaneByIdx(int laneIdx){
		float laneCenter = transform.localScale.z / 2f / trackCount;
		laneIdx = Mathf.Max (0, Mathf.Min (trackCount - 1, laneIdx));
		return transform.position.z - transform.localScale.z / 2  + laneCenter * (laneIdx * 2f + 1f);
	}
	/**
	 * Retorna el carril adyacente superior/izquierdo
	 * */
	public float GetLaneAbove(float curLane) {
		//Primero en que carril está y cuál es el above
		return GetLaneByIdx( GetLaneIdx( curLane ) + 1);
	}

	/**
	 * Retorna el carril adyacente inferior/derecho
	 * */
	public float GetLaneBelow(float curLane) {
		//Primero en que carril está
		return GetLaneByIdx( GetLaneIdx( curLane ) - 1);
	}

	// Update is called once per frame
	void Update () {
		for(int i = 0;i< trackCount;i++){
			float lane = GetLaneByIdx(i);
			 Vector3 start = new Vector3(startPos, transform.position.y, lane);
			 Vector3 end = new Vector3(transform.position.x, transform.position.y, lane);
			
			 Debug.DrawLine(start,end, Color.red);
		}
	}
}
