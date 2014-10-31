using UnityEngine;
using System.Collections;

[RequireComponent (typeof (RunnerController))]
public class RunnerKeyboardTwoInput : AbstractRunnerInput
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

}
