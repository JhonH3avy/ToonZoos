using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator), typeof (Renderer), typeof (BoxCollider))]
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

	#region Variables
	public float speed = 1.0f;
	public float changeTrackSmooth = 1.0f;
	public float nextPosDistance = 0.2F;
	public float timeDelay = 0.2f;

	[SerializeField]
	private float speedFactor = 1.0f;
	[SerializeField]
	private float fallingDistance = 1.5f;
	[SerializeField]
	public CharacterState curState{ get; private set;}

	private CharacterState nextState;

	private TrackController tracker;

	private float nextX;
	private float currZ;
	private float nextZ;
	private float nextTime;
	
	private Animator anim;

	private Collider _collider;
	private Transform _transform;
	#endregion

	private void Awake ()
	{
		anim = GetComponent<Animator> ();
		tracker = TrackController.instance;
		_collider = GetComponent<BoxCollider> ();
		_transform = transform;
	}

	private void Start ()
	{
		curState = CharacterState.Ready;
		nextTime = Time.timeSinceLevelLoad + timeDelay;

	}

	#region Enable and Disable callbacks

	private void OnEnable ()
	{
		TrackController.RaceStarted += StartRunning;
		TrackController.PreparingRace += () => {
			currZ = tracker.GetAvailableLane ();
			nextX = tracker.GetStartLineX ();
			Debug.Log ("Me ubique");

		};
	}

	private void OnDisable ()
	{
		TrackController.RaceStarted -= StartRunning;
	}

    #endregion

	#region Commands

	public void TrackUp ()
	{
		var newZ = tracker.GetLaneAbove (currZ);
		if (CheckNextTrack (newZ, transform.forward))
		{
			nextZ = newZ;
			nextState = CharacterState.Changing_Track;
		}
	}

	public void TrackDown ()
	{
		var newZ = tracker.GetLaneBelow (currZ);
		if (CheckNextTrack (newZ, -transform.forward))
		{
			nextZ = newZ;
			nextState = CharacterState.Changing_Track;
		}
	}

	public void Jump ()
	{
		nextState = CharacterState.Jumping;
	}

	#endregion

	private void StartRunning ()
	{
		nextState = CharacterState.Running;
		nextX = _transform.position.x + speed * speedFactor * (timeDelay);
		anim.SetTrigger ("IsRunning");
	}

	private void Update ()
	{
		if (Time.timeSinceLevelLoad >= nextTime)
		{
			if (nextState != curState)
			{
				ChangeState ();
			}
			ExecuteState ();
		}

		// En todo momento se estara realizando una aproximacion al lugar al cual el jugador debe de estar cada 200 millis
		_transform.position = Vector3.Lerp (_transform.position, new Vector3 (nextX, _transform.position.y, currZ), Time.deltaTime);
		anim.SetFloat ("Speed", speedFactor);
	}

	#region States implementation

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
			nextTime = Time.timeSinceLevelLoad + timeDelay;
			// Para pasar al estado de correr, se necesita hacer Click/Touch en el jugador
			speedFactor = 0.0f;
			break;

		case CharacterState.Ready:
			nextTime = Time.timeSinceLevelLoad + timeDelay;
			// El estado de preparacion no realizara ni permitira al usuario realizar accion alguna
			break;

		case CharacterState.Running:
			nextTime = Time.timeSinceLevelLoad + timeDelay;
			nextX = _transform.position.x + speed * speedFactor * (timeDelay);
			break;

		case CharacterState.Jumping:
			nextTime = Time.timeSinceLevelLoad + timeDelay;
			anim.SetTrigger ("Jump");
			nextState = CharacterState.Running;
			break;

		case CharacterState.Changing_Track:
			nextTime = Time.timeSinceLevelLoad + timeDelay;
			nextX = _transform.position.x + speed * speedFactor * (timeDelay);
			currZ = nextZ;
			nextState = CharacterState.Running;
			break;
		}
	}

	#endregion

	#region Obstacle Effects
	 /*
	  * Esta funcion hara caer al personaje poniendolo en un estado de espera paara que
	  * el jugador lo levante
	  * */
	public void Fall ()
	{
		nextX = _transform.position.x + speed * speedFactor * (timeDelay) + fallingDistance;
		anim.SetBool ("Fall", true);
		StopCoroutine ("RaiseSpeed");
		nextState = CharacterState.Falling;
	}

	/*
	 * Funcion que levanta al personaje una vez el jugador ha hecho click/touch encima del personaje
	 * */
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

	/*
	 * Esta funcion ralentizara al personaje dependiendo del valor que se le pase como parametro
	 * @params float
	 * */
	public void SlowDown (float newSpeedFactor)
	{
		StopCoroutine ("RaiseSpeed");
		speedFactor = newSpeedFactor;
		StartCoroutine ("RaiseSpeed");
	}

	private IEnumerator RaiseSpeed ()
	{
		while (speedFactor < 1.0f)
		{
			speedFactor = Mathf.Lerp (speedFactor, 1.0f, 0.5f * Time.deltaTime);
			yield return null;
		}
	}

	#endregion

	#region Change track implementation
	/*
	 * La funcion recibe la nueva posicion en Z y un vector de direccion para saber si en el carril
	 * al que pertenece el nuevo Z se encuentra un personaje. Si hay un personaje y menos del 50% de el
	 * se encuetra delante o detras de este jugador, retorna true, sino retorna false
	 * @params float, Vector3
	 * @return bool
	 * */
	private bool CheckNextTrack (float newZ, Vector3 direction)
	{
		var distance = Mathf.Abs (newZ - _transform.position.z);
		RaycastHit hit;

		// El rayo saldra del centro del sprite para saber
		if (Physics.Raycast (_collider.bounds.center, direction, out hit, distance))
		{
			// Si el rayo golpea a un jugador no podra pasar al siguiente carril
			return false;
		}
		// Si el rayo no golpea a un jugador, se puede pasar al siguiente carril
		return true;
	}

	/*
	 * Al momento de pasar a otro carril el jugador que se encuentre delante puede tocar el
	 * collider del jugador que se encuentre atras para ralentizarlo
	 * */
	private void OnTriggerEnter (Collider other)
	{
		if (other.transform.tag.Contains("Player"))
		{
			if ((other.rigidbody.position - _transform.position).x > 0)
			{
				SlowDown (0.5f);
			}
		}
	}
	#endregion
}
