using UnityEngine;
using System.Collections;

/**
 * Se encarga de controlar los eventos y las características propias de la pista d carreras.
 * 
 * Asigna carriles 
 * Da la información de que carril queda arriba y abajo de un carril dado
 * 
 * */
public class TrackController : MonoBehaviour {

	private static TrackController _instance;

	public static TrackController instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<TrackController>();
				
				//Tell unity not to destroy this object when loading a new scene!
				DontDestroyOnLoad(_instance.gameObject);
			}
			
			return _instance;
		}
	}


	public float trackLength = 20;
	public int trackCount = 4;

	private float startPos = 0;
	private int freeLane = 0;

	#region Events and EventsHanlder

	public delegate void RaceEventHandler ();
	public delegate void RaceFinishtHandler (FinishingRaceArgs args);

	public static event RaceEventHandler PreparingRace;
	public static event RaceEventHandler RaceStarted;
	public static event RaceFinishtHandler FinishingRace;
	

	protected void fire(RaceEventHandler handler)
	{
		if (handler != null)
			handler.Invoke ();
	}

	#endregion
	private void Awake ()
	{
		// If instance already exist, destroy ourself.
		if (_instance != null && _instance != this)			
		{ 
			Destroy(gameObject);
			return;
		}
		// No instance exist yet? We are it.
		_instance = this;
		
		// This line exist so that the Singleton would persist between scene loads.
		// Not all singletons needs that.
		DontDestroyOnLoad(gameObject); 
	}
	
	public float GetFinishLineX(){
		return transform.position.x;
	}

	public float GetStartLineX(){
		return startPos;
	}

	/**
	 * Retorna una posición que está en alguna proporción de un carril dado.
	 * 
	 * @param lane el número del carril
	 * @param perc el porcentaje del carril de la posición requerida
	 * */
	public Vector3 GetPointOnLane(int lane, float y, float perc){
		float laneX = trackLength* Mathf.Max(0, Mathf.Min(1,perc));
		return new Vector3(transform.position.x - laneX, y, GetLaneByIdx (lane));
	}


	/**
	 * Retorna el numero del carril empezando en cero
	 * */
	public int GetLaneIdx(float curLane){
		float halfCnt = (trackCount - 1)/2f;
		return (int)( (curLane-transform.position.z) * trackCount / transform.localScale.z + halfCnt );
	}

	public float GetLaneByIdx(int laneIdx){
		float laneCenter = transform.localScale.z / 2f / trackCount;
		laneIdx = Mathf.Max (0, Mathf.Min (trackCount - 1, laneIdx));
		return transform.position.z - transform.localScale.z / 2  + laneCenter * (laneIdx * 2f + 1f);
	}

	public float GetAvailableLane(){
		if(freeLane < trackCount)
			return GetLaneByIdx(freeLane++);
		//No hay más disponibles!
		return float.NaN;
	}

	/**
	 * Retorna el carril adyacente superior/izquierdo
	 * */
	public float GetLaneAbove(float curLane) {
		print ("CurLane Above prm "+  curLane );
		print ("CurLane Above "+ GetLaneIdx( curLane ));
		//Primero en que carril está y cuál es el above
		return GetLaneByIdx( GetLaneIdx( curLane ) + 1);
	}

	/**
	 * Retorna el carril adyacente inferior/derecho
	 * */
	public float GetLaneBelow(float curLane) {
		print ("CurLane Below prm "+  curLane );
		print ("CurLane Below "+ GetLaneIdx( curLane ));
		//Primero en que carril está
		return GetLaneByIdx( GetLaneIdx( curLane ) - 1);
	}

	private IEnumerator Start () {
		trackCount = Mathf.Max(1, Mathf.Min(4, trackCount));
		trackLength = Mathf.Abs(trackLength);
		startPos = transform.position.x;
		transform.position += Vector3.right * trackLength;

		print("TrackController started, preparing race");

		fire (PreparingRace);
		yield return new WaitForSeconds (3.0f);
		fire (RaceStarted);

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

	private void OnTriggerEnter (Collider c)
	{
		if(c.transform.tag.Contains("Player"))
		{
			Debug.Log(" We have a winner:" +c.gameObject);
			if(FinishingRace != null)
				FinishingRace(new FinishingRaceArgs(c.gameObject));
		}
	}
}

public class FinishingRaceArgs : System.EventArgs
{
	public FinishingRaceArgs(GameObject winner)
	{
		this.Winner = winner;
	}
	public GameObject Winner{get;private set;}
}