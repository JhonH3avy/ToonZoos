using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackController : Singleton<TrackController>
{
	public List<GameObject> players;
	public List<Transform> trackBenchamarks;

	#region Events and EventsHanlder
	
	public delegate void RaceEventHandler ();
	
	public static event RaceEventHandler RaceStarting;
	
	protected virtual void OnRaceStarting ()
	{
		RaceEventHandler hanlder = RaceStarting;
		if (hanlder != null)
		{
			hanlder.Invoke ();
		}
	}
	
	public static event RaceEventHandler RaceStarted;
	
	protected virtual void OnRaceStarted ()
	{
		RaceEventHandler hanlder = RaceStarted;
		
		if (hanlder != null)
		{
			hanlder.Invoke ();
		}
	}
	
	public static event RaceEventHandler RaceFinishing;
	
	protected virtual void OnRaceFinishing ()
	{
		RaceEventHandler handler = RaceStarting;
		
		if (handler != null)
		{
			handler.Invoke ();
		}
	}
	
	#endregion

	private void Awake ()
	{
		if (Instance == this)
		{
			DontDestroyOnLoad (gameObject);
		}
		else
		{
			DestroyImmediate (gameObject);
		}
	}

	private IEnumerator Start ()
	{
		for (int i = 0; i < players.Count; i++)
		{
			players[i].transform.position = trackBenchamarks[i].position;
			players[i].GetComponent<RunnerController> ().trackIndex = i;
			Debug.Log ("Los posiciono");
		}

		yield return new WaitForSeconds (2.0f);
		OnRaceStarted ();
	}

	public Vector3 AssignTrack (int trackIndex)
	{
		return trackBenchamarks[trackIndex].position;
	}
}
