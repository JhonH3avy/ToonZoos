using UnityEngine;
using System.Collections;


/**
 * Es la base para todos los tipos de runner input controllers
 * */
[RequireComponent ( typeof ( PowerFactory ), typeof ( RunnerController ) )]
public class AbstractRunnerInput : MonoBehaviour
{
    private RunnerController controller;
    private PowerFactory powerFactory;


    private void Awake ()
    {
        controller = GetComponent<RunnerController> ();
        powerFactory = GetComponent<PowerFactory> ();
        if ( controller == null )
            throw new MissingReferenceException ( "A RunnerController is required" );
        if ( powerFactory == null )
            Debug.LogError ( "A PowerFactory is required in the runner" );
    }

    protected void LaneUp ()
    {
        controller.TrackUp ();
    }

    protected void LaneDown ()
    {
        controller.TrackDown ();
    }

    protected void Fire ()
    {
        if ( powerFactory != null )
            powerFactory.Fire ();
    }

    protected void Jump ()
    {
        if ( controller.CurrentState == RunnerController.CharacterState.Falling )
            controller.GetUp ();
        else
            controller.Jump ();
    }

    protected void GetUp ()
    {
        controller.GetUp ();
    }
}
