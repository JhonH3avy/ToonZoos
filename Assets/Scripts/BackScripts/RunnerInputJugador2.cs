using UnityEngine;
using System.Collections;

//FIXME deberia llamarse RunnerKeyboardTwoInput
[RequireComponent (typeof (RunnerController))]
public class RunnerInputJugador2 : AbstractRunnerInput
{
	private void FixedUpdate ()
	{
		if (Input.GetButtonDown ("Vertical2"))
		{
			if (Input.GetAxisRaw ("Vertical2") == 1f)
				LaneUp();
			
			if (Input.GetAxisRaw ("Vertical2") == -1f)
				LaneDown();
		}
		
		if (Input.GetButtonDown ("Jump2"))
			Jump();
		
		if (Input.GetButtonDown ("Ability2"))
			Fire();
	}
	
	private void OnMouseUpAsButton ()
	{	
		// El metodo GetUp solo reacionara cuando el personaje se encuentre en el estado Falling
		GetUp ();
	}
}
