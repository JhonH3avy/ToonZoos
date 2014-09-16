using UnityEngine;
using System.Collections;

public class PanelHUD : MonoBehaviour
{
	public GameManager manager;

	private void Awake ()
	{
		manager.RaceStarting += HidePanel;
	}

	private void HidePanel ()
	{
		gameObject.SetActive (false);
	}
}
