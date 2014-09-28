using UnityEngine;
using System.Collections;

[RequireComponent (typeof (RunnerController))]
public class RunnerInput : MonoBehaviour
{
	private TrackControllerB tracker;
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
			//controller.Jump ();
		}
		
		if (Input.GetButtonDown ("Fire1"))
		{
			//controller.Ability ();
		}
	}
}
