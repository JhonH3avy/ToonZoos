using UnityEngine;
using Menu.Button;
using System.Collections;

public class MenuController : MonoBehaviour
{
	[SerializeField]
	private GameObject menuPrincipal;
	[SerializeField]
	private GameObject seleccionPersonaje;

	[SerializeField]
	private Button _buttonRace;
	[SerializeField]
	private Button _buttonVs;
	[SerializeField]
	private Button _buttonUser;

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
	}
}
