using UnityEngine;
using System.Collections;

public class MenuTrasition : MonoBehaviour
{
	private Animator anim;

	#region Events and EventsHanlder
	public delegate void TransitionEventHandler ();
	public static event TransitionEventHandler TransitionEnded;

	protected virtual void OnTransitionEnded ()
	{
		TransitionEventHandler handler = TransitionEnded;

		if(handler != null)
		{
			handler.Invoke();
		}
	}
	#endregion

	private void Awake ()
	{
		anim = GetComponent<Animator> ();

		TransitionEnded += SetStay;
	}

	public void TransitionEnd ()
	{
		OnTransitionEnded();
	}

	private void SetStay ()
	{
		anim.SetTrigger ("Stay");
	}
}
