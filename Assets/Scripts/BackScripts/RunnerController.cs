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
	public float changeTrackSmooth = 1.0f;
	public float nextPosDistance = 0.2F;
	public float timeDelay = 0.2f;
	public float speedFactor
	{
		get
		{
			return _speedFactor;
		}

		private set
		{
			_speedFactor = value;
			StopCoroutine ("RaiseSpeed");
			StartCoroutine ("RaiseSpeed");
		}
	}

	[SerializeField]
	private float _speedFactor = 1.0f;
	private CharacterState curState;
	private CharacterState nextState;
	private TrackController tracker;
	private float nextX;
	private float currZ;
	private float nextZ;
	private Animator anim;
	private float nextTime;

	private void Awake ()
	{
		anim = GetComponent<Animator> ();
		tracker = TrackController.instance;
	}

	private void Start ()
	{
		curState = CharacterState.Ready;
		currZ = tracker.GetAvailableLane ();
		nextTime = Time.time * timeDelay;
	}

	#region Enable/Disable

	private void OnEnable ()
	{
		TrackController.RaceStarted += StartRunning;
	}

	private void OnDisable ()
	{
		TrackController.RaceStarted -= StartRunning;
	}

    #endregion

	#region Commands

	public void TrackUp ()
	{
		nextZ = tracker.GetLaneAbove (currZ);
		nextState = CharacterState.Changing_Track;
	}

	public void TrackDown ()
	{
		nextZ = tracker.GetLaneBelow (currZ);
		nextState = CharacterState.Changing_Track;
	}

	public void Jump ()
	{
		nextState = CharacterState.Jumping;
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

		transform.position = Vector3.Lerp (transform.position, new Vector3 (nextX, transform.position.y, currZ), speedFactor * Time.deltaTime);
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
			if (curState == CharacterState.Ready || curState == CharacterState.Jumping)
				curState = nextState;
			break;

		case CharacterState.Jumping:
			if (curState == CharacterState.Running)
				curState = nextState;
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
			anim.SetTrigger ("Jump");
			nextState = CharacterState.Running;
			break;

		case CharacterState.Changing_Track:
			nextX = speed * speedFactor * (Time.time + timeDelay);
			currZ = nextZ;
			nextState = CharacterState.Running;
			break;
		}
	}

	public void SlowDown (float slowFactor)
	{
		speedFactor = slowFactor;
	}

	private IEnumerator RaiseSpeed ()
	{
		while (!(speedFactor >= 1.0f))
		{
			yield return new WaitForSeconds (0.1f);
			_speedFactor += 0.01f;
			_speedFactor = Mathf.Clamp01 (_speedFactor);
		}
	}
}
