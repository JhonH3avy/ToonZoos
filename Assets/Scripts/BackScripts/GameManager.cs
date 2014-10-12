using System.Collections.Generic;
using UnityEngine;
using System.Collections;
using Menu.Characters;

public class GameManager : MonoBehaviour
{
	private static GameManager _instance;
	// Singleton de la instancia del GameManager
	public static GameManager instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = GameObject.FindObjectOfType<GameManager>();
				
				if (_instance == null)
				{
					Debug.LogError("An instance of " + typeof(GameManager) + 
					               " is needed in the scene, but there is none.");
				}
				else
				{
					DontDestroyOnLoad (_instance.gameObject);
				}
			}
			
			return _instance;
		}
	}

	public int playersCount;

	private List<Player> _players;

	public void PlayerSelection (Characters charPlayer)
	{
		Player currPlayer = GetNextPlayer ();

		if (currPlayer != null)
		{
			currPlayer.character = charPlayer;
			Debug.Log (string.Format ("Player {0} selected {1}", currPlayer.playerIndex, charPlayer.ToString ()));
		}
	}

	/*
	 * Esta funcion devuelve al siguiente jugador que no haya elegido a un personaje
	 * para que se le pueda asignar
	 * */
	private Player GetNextPlayer ()
	{
		foreach (var player in _players)
		{
			if (player.character == Characters.None)
				return player;
		}
		StartCoroutine (StartGame ());	// SI no hay mas jugadores disponibles se empieza el juego

		return null;
	}

	private void OnEnable ()
	{
		if (instance != this)
		{
			Destroy (gameObject);
		}

		if (_players == null)
			_players = new List<Player> ();

		// Llenamos la lista de jugadores hasta la cantidad de jugadores que esten en la variable playersCount
		for (int i = 0; i < playersCount; i++)
		{
			_players.Add (new Player (Characters.None, i + 1));
		}
	}

	private void OnDisable ()
	{
		_players.Clear ();
	}

	private IEnumerator StartGame ()
	{
		yield return new WaitForSeconds (2.0f);
		Application.LoadLevel ("Dos Jugadores");
	}
}
