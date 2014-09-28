using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator), typeof (Renderer))]
public class RunnerController : MonoBehaviour
{
	public enum CharacterState
	{
		Ready,
		Running,
		Falling,
		Jumping,
		Changing_Track
	}

	public float speed = 1.0f;
	public float speedFactor = 1.0f;
	public float changeTrackSmooth = 1.0f;
	public float nextPosDistance = 0.2F;
	public float timeDelay = 0.2f;

	private CharacterState curState;
	private CharacterState nextState;
	private TrackControllerB tracker;
	private float nextX;
	private float currZ;
	private float nextZ;
	private Animator anim;
	private float nextTime;

	private void Awake ()
	{
		anim = GetComponent<Animator> ();
		tracker = TrackControllerB.Instance;
	}

	private void Start ()
	{
		curState = CharacterState.Ready;
		currZ = tracker.GetAvailableTrack ();
		nextTime = Time.time * timeDelay;
	}

	#region Enable/Disable

	private void OnEnable ()
	{
		TrackControllerB.RaceStarted += StartRunning;
	}

	private void OnDisable ()
	{
		TrackControllerB.RaceStarted -= StartRunning;
	}

    #endregion

	#region Commands

	public void TrackUp ()
	{
		nextZ = tracker.GetTrackAbove (currZ);
		nextState = CharacterState.Changing_Track;
	}

	public void TrackDown ()
	{
		nextZ = tracker.GetTrackBelow (currZ);
		nextState = CharacterState.Changing_Track;
	}

	#endregion

	private void StartRunning ()
	{
		nextState = CharacterState.Running;
		nextX = speed * speedFactor * (Time.time + timeDelay);
		anim.SetTrigger ("IsRunning");
	}

	private void Update ()
	{
		if (Time.time >= nextTime)
		{
			if (nextState != curState)
			{
				ChangeState ();
			}
			ExecuteState ();
		}

		transform.position = Vector3.Lerp (transform.position, new Vector3 (nextX, transform.position.y, currZ), speed * Time.deltaTime);
	}

	private void ChangeState ()
	{
		switch (nextState)
		{
		case CharacterState.Falling:

			break;

		case CharacterState.Ready:

			break;

		case CharacterState.Running:
			if (curState == CharacterState.Ready)
				curState = nextState;
			break;

		case CharacterState.Jumping:

			break;

		case CharacterState.Changing_Track:
			if (curState == CharacterState.Running)
				curState = nextState;
			break;
		}
	}

	private void ExecuteState ()
	{
		switch (curState)
		{
		case CharacterState.Falling:

			break;

		case CharacterState.Ready:

			break;

		case CharacterState.Running:
			nextX = speed * speedFactor * (Time.time + timeDelay);
			break;

		case CharacterState.Jumping:

			break;

		case CharacterState.Changing_Track:
			nextX = speed * speedFactor * (Time.time + timeDelay);
			currZ = nextZ;
			nextState = CharacterState.Running;
			break;
		}
	}
}
