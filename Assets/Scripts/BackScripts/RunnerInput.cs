﻿using UnityEngine;
using System.Collections;

[RequireComponent (typeof (RunnerController))]
public class RunnerInput : MonoBehaviour
{
	private TrackController tracker;
	private RunnerController controller;

	private void Awake ()
	{
		controller = GetComponent<RunnerController> ();
	}

	private void FixedUpdate ()
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
		
		if (Input.GetButtonDown ("Ability"))
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
