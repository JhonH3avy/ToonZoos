using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Scripts.Interfaces;

public class PlayerManager : Singleton<PlayerManager>
{
	public static List<IPlaceable> players;
	public List<Transform> tracks;

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

		players = new List<IPlaceable> ();

		List<GameObject> playersGO = new List<GameObject> ();
		playersGO.AddRange (GameObject.FindGameObjectsWithTag ("Player"));

		foreach (GameObject playerGO in playersGO)
		{
			players.Add ((IPlaceable) playerGO.GetComponent<PlayerController> ());
		}
	}

	private void OnEnable ()
	{
		GameManager.RaceStarting += PlacePlayers;
	}

	private void OnDisable ()
	{
		GameManager.RaceStarting -= PlacePlayers;
	}

	private void PlacePlayers ()
	{
		int index = 0;

		foreach(PlayerController player in players)
		{
			player.PlaceInTrack (tracks[index].position, index);
			index++;
		}
	}
}
