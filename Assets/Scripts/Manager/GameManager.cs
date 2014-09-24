using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class GameManager : Singleton<GameManager>
{
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

	private void Start ()
	{
		OnRaceStarting ();
		StartDelay ("DelayRaceStart");

	}

	private IEnumerator DelayRaceStart ()
	{
		yield return new WaitForSeconds (3.0f);
		
		OnRaceStarted ();
	}

	private void StartDelay (string method)
	{
		StartCoroutine (method);
	}
}
