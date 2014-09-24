using UnityEngine;
using System.Collections;
using Scripts.Interfaces;

[RequireComponent (typeof( PlayerController ))]
public class PlayerInput : MonoBehaviour
{
	private ICommand controller;

	private void Awake ()
	{
		controller = (ICommand) GetComponent<PlayerController> ();
	}

	private void Update ()
	{
		if (Input.GetButtonDown ("Vertical"))
		{
			if (Input.GetAxisRaw ("Vertical") == 1f)
			{
				controller.TrackUp ();
			}

			if (Input.GetAxisRaw ("Vertical") == -1f)
			{
				controller.TrackDown ();
			}
		}

		if (Input.GetButtonDown ("Jump"))
		{
			controller.Jump ();
		}

		if (Input.GetButtonDown ("Fire1"))
		{
			controller.Ability ();
		}
	}
}
