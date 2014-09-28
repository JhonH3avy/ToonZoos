using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TrackControllerB : Singleton<TrackControllerB>
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

	public float GetAvailableTrack () 
	{
		return 0;
	}

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
	public float GetTrackAbove(float track){
		//TODO basado en la altura del collider, retornar el track.Y que queda justo encima 
		//	de "track" dado el número de carriles. Si supera el borde, retorna el borde
		return 1 + track;
	}
	
	public float GetTrackBelow(float track){
		//TODO basado en la altura del collider, retornar el track.Y que queda justo encima 
		//	de "track" dado el número de carriles. Si supera el borde, retorna el borde
		return track - 1;	
	}

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
		yield return new WaitForSeconds (2.0f);
		OnRaceStarted ();
	}
}
