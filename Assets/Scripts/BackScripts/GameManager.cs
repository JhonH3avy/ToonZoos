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
	private Player currPlayer;

	/*
	 * Funcion que sirve para que es llamada por los items del scrollview de seleccion de personaje
	 * indicando que un personaje fue elegido y se debe a elejir el persoanje del siguiente jugador
	 * */
	public void PlayerSelection (Characters charPlayer)
	{
		if (currPlayer != null)
		{
			currPlayer.character = charPlayer;
			Debug.Log (string.Format ("Player {0} selected {1}", currPlayer.playerIndex, charPlayer.ToString ()));	
			currPlayer = GetNextPlayer ();
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

		return null;
	}

	public Player GetCurrentPlayer ()
	{
		return currPlayer;
	}

	public int GetRemainingPlayers ()
	{
		var i = 0;
		foreach (var player in _players)
			if (player.character == Characters.None)
				i++;

		return i;
	}

	public void ResetPlayersCharacter ()
	{
		foreach (var player in _players)
		{
			player.character = Characters.None;

			if (player.playerIndex == 1)
				currPlayer = player;
		}
	}

	private void Awake ()
	{
		if (instance != this)
		{
			Destroy (gameObject);
		}

		_players = new List<Player> ();

		// Llenamos la lista de jugadores hasta la cantidad de jugadores que esten en la variable playersCount
		for (int i = 0; i < playersCount; i++)
		{
			_players.Add (new Player (Characters.None, i + 1));
		}	

		currPlayer = GetNextPlayer ();
	}

	private void OnEnable ()
	{
		ResetPlayersCharacter ();
	}

	private void OnDisable ()
	{
		_players.Clear ();
		currPlayer = null;
	}

	private void OnLevelWasLoaded (int level)
	{
		if (level == 1)
		{
			CreatePlayers ();
		}
	}

	private void CreatePlayers ()
	{
		foreach (var player in _players)
		{
			var prefab = Resources.Load<GameObject> ("Prefabs/Players/Player" + player.character.ToString());
			if(prefab != null)
			{
				var PlayerIns = (GameObject) Instantiate (prefab);
				PlayerIns.name = string.Format ("Player {0} {1}", player.playerIndex, player.character);
				if (player.playerIndex == 1)
					PlayerIns.AddComponent<RunnerInput> ();
				else
					PlayerIns.AddComponent<RunnerInputJugador2> ();
			}
			else
				Debug.LogError ("No se encontro el objeto Player" + player.character.ToString());
		}
	}
}
