using UnityEngine;
using System.Collections;

[RequireComponent (typeof (Animator), typeof (Renderer))]
public class RunnerController : MonoBehaviour
{
	public float speed = 2.0f;
	public float changeTrackSmooth = 1.0f;
	public bool isRunning;

	private TrackController tracker;
	private int _trackIndex;
	private Animator anim;

	public int trackIndex
	{
		set
		{
			_trackIndex = value;
			renderer.sortingOrder = value;
		}

		get
		{
			return _trackIndex;
		}
	}

	private void Awake ()
	{
		anim = GetComponent<Animator> ();
		tracker = TrackController.Instance;
		tracker.players.Add (gameObject);
	}

	private void Start ()
	{
		isRunning = false;
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
		if (trackIndex <= 0)
			return;

		trackIndex--;
		Vector3 newPos = tracker.AssignTrack (trackIndex);
		StopCoroutine ("ChangeTrack");
		StartCoroutine ("ChangeTrack", newPos);
	}

	public void TrackDown ()
	{
		if (trackIndex >= 3)
			return;

		trackIndex++;
		Vector3 newPos = tracker.AssignTrack (trackIndex);
		StopCoroutine ("ChangeTrack");
		StartCoroutine ("ChangeTrack", newPos);
	}

	public void Jump ()
	{
		anim.SetTrigger ("Jump");
	}

	public void Ability ()
	{

	}

	#endregion

	private void StartRunning ()
	{
		isRunning = true;
		anim.SetTrigger ("IsRunning");
		StartCoroutine ("Run");
	}

	public void StopRunning ()
	{
		StopCoroutine ("Run");
	}

	private IEnumerator Run ()
	{
		while (isRunning)
		{
			transform.Translate (Vector3.right * speed * Time.deltaTime);
			yield return null;
		}
	}
		
	private IEnumerator ChangeTrack (Vector3 targetPos)
	{
		targetPos = new Vector3 (transform.position.x, targetPos.y, 0.0f);

		while (targetPos != transform.position)
		{
			transform.position = Vector3.Lerp (transform.position, targetPos, changeTrackSmooth * Time.deltaTime);
			yield return null;
			targetPos = new Vector3 (transform.position.x, targetPos.y, 0.0f);
		}
	}

	public void SlowDown ()
	{
		speed *= 0.5f;
	}
}
