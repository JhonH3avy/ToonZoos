using UnityEngine;
using System.Collections;

//FIXME deberia llamarse RunnerKeyboardOneInput
[RequireComponent (typeof (RunnerController))]
public class RunnerInput : AbstractRunnerInput
{
	private void FixedUpdate ()
	{
		if (Input.GetButtonDown ("Vertical"))
		{
			if (Input.GetAxisRaw ("Vertical") == 1f)
				LaneUp ();
			
			if (Input.GetAxisRaw ("Vertical") == -1f)
				LaneDown ();
		}
		
		if (Input.GetButtonDown ("Jump"))
			Jump ();

		if (Input.GetButtonDown ("Ability"))
			Fire();
	}

	private void OnMouseUpAsButton ()
	{	
		// El metodo GetUp solo reacionara cuando el personaje se encuentre en el estado Falling
		GetUp ();
	}
}
