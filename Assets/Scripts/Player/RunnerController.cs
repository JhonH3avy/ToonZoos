using UnityEngine;
using System.Collections;

[RequireComponent ( typeof ( BoxCollider ), typeof ( Rigidbody ) )]
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
	private CharacterState _curState;
	public CharacterState curState
	{ 
		get
		{
			return _curState;
		} 
		private set
		{
			_curState = value;
		}
	}
	
	private CharacterState nextState;

	private TrackController tracker;

	private float nextX;
	private float currZ;
	private float nextZ;
	private float nextTime;
	
	private Animator _animator;
	private Collider _collider;
	private Transform _transform;
	#endregion

	#region Unity API callbacks
	private void Awake ()
	{
		tracker = TrackController.instance;
		_animator = GetComponentInChildren<Animator> ();
		_collider = GetComponent<BoxCollider> ();
		_transform = transform;
	}

	private void Start ()
	{
		curState = CharacterState.Ready;
		nextTime = Time.timeSinceLevelLoad + timeDelay;

	}

	private void Update ()
	{
		if ( Time.timeSinceLevelLoad >= nextTime )
		{
			if ( nextState != curState )
			{
				ChangeState ();
			}
			ExecuteState ();
		}

		// En todo momento se estara realizando una aproximacion al lugar al cual el jugador debe de estar cada 200 millis
		_transform.position = Vector3.Lerp ( _transform.position, new Vector3 ( nextX, _transform.position.y, currZ ), Time.deltaTime );
		_animator.SetFloat ( "Speed", speedFactor );
	}

	#region Enable and Disable callbacks

	private void OnEnable ()
	{
		TrackController.RaceStarted += StartRunning;
		TrackController.PreparingRace += () => {
			currZ = tracker.GetAvailableLane ();
			nextX = tracker.GetStartLineX ();
		};
	}

	private void OnDisable ()
	{
		TrackController.RaceStarted -= StartRunning;
	}

    #endregion
	#endregion

	#region Commands
	public void TrackUp ()
	{
		var newZ = tracker.GetLaneAbove (currZ);
		if (CheckNextTrack (newZ, Vector3.forward))
		{
			nextZ = newZ;
			nextState = CharacterState.Changing_Track;
			StartCoroutine ( "ChangingTrack" );
		}
	}

	public void TrackDown ()
	{
		var newZ = tracker.GetLaneBelow (currZ);
		if (CheckNextTrack (newZ, -Vector3.forward))
		{
			nextZ = newZ;
			nextState = CharacterState.Changing_Track;
			StartCoroutine ( "ChangingTrack" );
		}
	}

	public void Jump ()
	{
		nextState = CharacterState.Jumping;
		_animator.SetTrigger ( "Jump" );
		StartCoroutine ( "Jumping" );
	}
	#endregion

	#region HelperMethods
	private void StartRunning ()
	{
		nextState = CharacterState.Running;
		nextX = _transform.position.x + speed * speedFactor * (timeDelay);
		_animator.SetTrigger ("IsRunning");
	}

	private IEnumerator Jumping ()
	{
		yield return new WaitForSeconds ( 1.5f );
		nextState = CharacterState.Running;
	}

	private IEnumerator ChangingTrack ()
	{
		yield return new WaitForSeconds ( 1.0f );
		nextState = CharacterState.Running;
	}
	#endregion

	#region States implementation
	private void ChangeState ()
	{
		switch (nextState)
		{
		case CharacterState.Falling:
			if (curState == CharacterState.Running || curState == CharacterState.Changing_Track
			    || curState == CharacterState.Jumping)
				curState = nextState;
			break;

		case CharacterState.Ready:
			// Cualquier estado puede pasar a Ready
			// TODO realizar un bloqueo para que cualquier estado pase a Ready
			break;

		case CharacterState.Running:
			if (curState == CharacterState.Ready || curState == CharacterState.Jumping
			    || curState == CharacterState.Falling || curState == CharacterState.Changing_Track)
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
		Debug.Log ("curState: " + curState);
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
			nextX = _transform.position.x + speed * speedFactor * ( timeDelay );
			break;

		case CharacterState.Changing_Track:
			nextTime = Time.timeSinceLevelLoad + timeDelay;
			nextX = _transform.position.x + speed * speedFactor * (timeDelay);
			currZ = nextZ;
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
		_animator.SetBool ("Fall", true);
		StopCoroutine ("RaiseSpeed");
		StopCoroutine ( "Jumping" );
		StartCoroutine ( "ChangingTrack" );
		nextState = CharacterState.Falling;
	}

	/*
	 * Funcion que levanta al personaje una vez el jugador ha hecho click/touch encima del personaje
	 * */
	public void GetUp ()
	{
		// Levantara al jugador cuando se le de click en el
		if (_animator.GetBool ("Fall"))
		{
			_animator.SetBool ("Fall", false);
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

		// El rayo saldra del centro del sprite para saber si hay un jugador
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
