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

	[SerializeField]
	private float fallDelay = 1.0f;
	[SerializeField]
	private float speedFactor = 1.0f;
	[SerializeField]
	private float fallingDistance = 1.5f;
	[SerializeField]
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
		nextTime = Time.time + timeDelay;
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
		nextX = transform.position.x + speed * speedFactor * (timeDelay);;
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

		// En todo momento se estara realizando una aproximacion al lugar al cual el jugador debe de estar cada 200 millis
		transform.position = Vector3.Lerp (transform.position, new Vector3 (nextX, transform.position.y, currZ), Time.deltaTime);
		anim.SetFloat ("Speed", speedFactor);
	}

	private void ChangeState ()
	{
		switch (nextState)
		{
		case CharacterState.Falling:
			if (curState == CharacterState.Running || curState == CharacterState.Changing_Track)
				curState = nextState;
			break;

		case CharacterState.Ready:
			// Cualquier estado puede pasar a Ready
			// TODO realizar un bloqueo para que cualquier estado pase a Ready
			break;

		case CharacterState.Running:
			if (curState == CharacterState.Ready || curState == CharacterState.Jumping
			    || curState == CharacterState.Falling)
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
			nextTime = Time.time + timeDelay;
			// Para pasar al estado de correr, se necesita hacer Click/Touch en el jugador
			speedFactor = 0.0f;
			break;

		case CharacterState.Ready:
			nextTime = Time.time + timeDelay;
			// El estado de preparacion no realizara ni permitira al usuario realizar accion alguna
			break;

		case CharacterState.Running:
			nextTime = Time.time + timeDelay;
			nextX = transform.position.x + speed * speedFactor * (timeDelay);
			break;

		case CharacterState.Jumping:
			nextTime = Time.time + timeDelay;
			anim.SetTrigger ("Jump");
			nextState = CharacterState.Running;
			break;

		case CharacterState.Changing_Track:
			nextTime = Time.time + timeDelay;
			nextX = transform.position.x + speed * speedFactor * (timeDelay);
			currZ = nextZ;
			nextState = CharacterState.Running;
			break;
		}
	}

	public void Fall ()
	{
		nextX = transform.position.x + speed * speedFactor * (timeDelay) + fallingDistance;
		anim.SetBool ("Fall", true);
		StopCoroutine ("RaiseSpeed");
		nextState = CharacterState.Falling;
	}

	public void GetUp ()
	{
		// Levantara al jugador cuando se le de click en el
		if (anim.GetBool ("Fall"))
		{
			anim.SetBool ("Fall", false);
			nextState = CharacterState.Running;
			StartCoroutine ("RaiseSpeed");
		}
	}

	private IEnumerator RaiseSpeed ()
	{
		while (speedFactor < 1.0f)
		{
			speedFactor = Mathf.Lerp (speedFactor, 1.0f, 0.5f * Time.deltaTime);
			yield return null;
		}
	}
}
