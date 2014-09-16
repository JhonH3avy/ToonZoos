using UnityEngine;
using UnityEngine.Advertisements;
using System.Collections;

public class GameManager : MonoBehaviour
{
	#region Events and EventsHanlder
	public delegate void RaceStartEventHandler ();
	public event RaceStartEventHandler RaceStarting;

	protected virtual void OnRaceStarting ()
	{
		RaceStartEventHandler hanlder = RaceStarting;
		if(hanlder != null)
		{
			hanlder.Invoke ();
		}
	}
	#endregion

	private void Start ()
	{
		RaceStarting += ShowAdvertisement;

		StartCoroutine(InitialCountdown ());

		Advertisement.Initialize("17022", true);
	}

	private IEnumerator InitialCountdown ()
	{
		yield return new WaitForSeconds (3.0f);
		
		OnRaceStarting ();
	}

	private void ShowAdvertisement ()
	{
		StartCoroutine(DelayAdvertisement());
	}

	private IEnumerator DelayAdvertisement ()
	{
		yield return new WaitForSeconds(3.0f);

		if(Advertisement.isReady())
		{
			Debug.Log("Advertisement was shownM");
			Advertisement.Show();
		}
	}
	

}
