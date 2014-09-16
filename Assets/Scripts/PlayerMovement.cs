using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
	public GameManager manager;

	private Animator anim;

	private void Awake ()
	{
		manager.RaceStarting += StartRunning;
	}

	private void Start ()
	{
		anim = GetComponent<Animator> ();
	}

	private void StartRunning ()
	{
		anim.SetTrigger ("IsRunning");
	}
}
