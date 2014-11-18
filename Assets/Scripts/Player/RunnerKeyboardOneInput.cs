using UnityEngine;
using System.Collections;

[RequireComponent ( typeof ( RunnerController ) )]
public class RunnerKeyboardOneInput : AbstractRunnerInput
{
    private void FixedUpdate ()
    {
        if ( Input.GetButtonDown ( "Vertical" ) )
        {
            if ( Input.GetAxisRaw ( "Vertical" ) == 1f )
                LaneUp ();

            if ( Input.GetAxisRaw ( "Vertical" ) == -1f )
                LaneDown ();
        }

        if ( Input.GetButtonDown ( "Jump" ) )
        {
            Jump ();
        }

        if ( Input.GetButtonDown ( "Ability" ) )
            Fire ();
    }

}
