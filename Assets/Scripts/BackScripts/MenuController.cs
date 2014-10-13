using UnityEngine;
using Menu.Button;
using System.Collections;

public class MenuController : MonoBehaviour
{
	[SerializeField] private GameObject menuPrincipal;
	[SerializeField] private GameObject seleccionPersonaje;

	[SerializeField] private Button _buttonRace;
	[SerializeField] private Button _buttonVs;
	[SerializeField] private Button _buttonUser;
	[SerializeField] private Button _buttonBack;

	private void Start ()
	{
		_buttonRace.Click += () => {
			menuPrincipal.SetActive (false);
			seleccionPersonaje.SetActive (true);
		};

		_buttonVs.Click += () => {
			//TODO rellenar funcion para que los jugadores puedan invitar a otros amigos
		};

		_buttonUser.Click += () => {
			//TODO hacer aparecer pantalla de usuario
		};

		_buttonBack.Click += () => {
			menuPrincipal.SetActive(true);
			seleccionPersonaje.SetActive(false);
			GameManager.instance.ResetPlayersCharacter ();
		};
	}

	private void Update ()
	{
		if (GameManager.instance.GetRemainingPlayers () <= 0)
		{
			StartCoroutine (StartGame ());
		}
	}

	private IEnumerator StartGame ()
	{
		yield return new WaitForSeconds (2.0f);
		Application.LoadLevel ("Dos Jugadores");
	}
}
