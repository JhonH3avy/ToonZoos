using UnityEngine;
using System.Collections;

[RequireComponent (typeof (RunnerController))]
public class RunnerInputJugador2 : MonoBehaviour
{
	private TrackController tracker;
	private RunnerController controller;
	
	private void Awake ()
	{
		controller = GetComponent<RunnerController> ();
	}
	
	private void FixedUpdate ()
	{
		
		if (Input.GetButtonDown ("Vertical2"))
		{
			if (Input.GetAxisRaw ("Vertical2") == 1f)
			{
				controller.TrackUp ();
			}
			
			if (Input.GetAxisRaw ("Vertical2") == -1f)
			{
				controller.TrackDown ();
			}
		}
		
		if (Input.GetButtonDown ("Jump2"))
		{
			controller.Jump ();
		}
		
		if (Input.GetButtonDown ("Ability2"))
		{
			//controller.Ability ();
		}
	}
	
	private void OnMouseUpAsButton ()
	{	
		// El metodo GetUp solo reacionara cuando el personaje se encuentre en el estado Falling
		controller.GetUp ();
	}
}
