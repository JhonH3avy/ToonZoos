using UnityEngine;
using System.Collections;
using Scripts.Interfaces;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour, ICommand, IPlaceable, IHurtable
{	
	public float speed = 1;

	private List<Transform> tracks;
	private int _currentTrackIndex;
	private Animator anim;
	private SpriteRenderer sprite;

	public int currentTrackIndex
	{
		get
		{
			return _currentTrackIndex;
		}

		set
		{
			sprite.sortingOrder = value;
			_currentTrackIndex = value;
		}
	}

	private void Awake ()
	{
		anim = GetComponent<Animator> ();
		sprite = GetComponent<SpriteRenderer> ();
	}

	private void Start ()
	{
		AssignTracks (GameObject.FindGameObjectWithTag ("PlayerManager").GetComponent<PlayerManager> ());
	}

	private void OnEnable ()
	{
		GameManager.RaceStarted += Run;
	}

	private void OnDisable ()
	{
		GameManager.RaceStarted -= Run;
	}

	#region ICommand implementation

	public event ButtonPressEventHandler ButtonPress;

	public void Jump ()
	{
	}

	public void Ability ()
	{
	}

	public void TrackUp ()
	{
		if (currentTrackIndex == 0)
			return;

		currentTrackIndex--;

		MoveInTrack (tracks[currentTrackIndex].position);
	}

	public void TrackDown ()
	{
		if (currentTrackIndex == tracks.Count - 1)
			return;

		currentTrackIndex++;

		MoveInTrack (tracks[currentTrackIndex].position);
	}

	#endregion

	#region IHurtable implementation

	public void SlowDown ()
	{
	}

	#endregion

	#region IPlaceable implementation
		
	public void PlaceInTrack (Vector3 initialTrackPosition, int orderInLayer)
	{
		transform.position = initialTrackPosition;
		currentTrackIndex = orderInLayer;
	}

	#endregion

	private void MoveInTrack (Vector3 newTrackPosition)
	{
		transform.position = newTrackPosition;
	}

	private void AssignTracks (PlayerManager manager)
	{
		this.tracks = manager.tracks;
	}

	private void Run ()
	{
		anim.SetTrigger ("IsRunning");
	}
}
