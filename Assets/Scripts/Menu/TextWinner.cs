using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UILabel))]
public class TextWinner : MonoBehaviour
{
	[SerializeField] private UILabel _label;
	[SerializeField] private UISprite _sprite;

	private void OnEnable ()
	{
		TrackController.FinishingRace += ShowWinner;
	}

	private void OnDisable ()
	{
		TrackController.FinishingRace -= ShowWinner;
	}

	private void Start ()
	{		
		_sprite.enabled = false;
	}

	private void ShowWinner (FinishingRaceArgs args)
	{
		_sprite.enabled = true;
		_label.text = "The Winner is " + args.Winner.name;
	}
}
