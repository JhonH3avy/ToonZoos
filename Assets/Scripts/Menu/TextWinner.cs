using UnityEngine;
using System.Collections;

[RequireComponent ( typeof ( UILabel ) )]
public class TextWinner : MonoBehaviour
{
    [SerializeField]
    private UILabel label;
    [SerializeField]
    private UISprite sprite;

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
        sprite.enabled = false;
    }

    private void ShowWinner (FinishingRaceEventArgs args)
    {
        sprite.enabled = true;
        label.text = "The Winner is " + args.Winner.name;
    }
}
