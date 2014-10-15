using UnityEngine;
using System.Collections;

[RequireComponent (typeof (UILabel))]
public class TitleText : MonoBehaviour
{
	UILabel label;

	private void Awake ()
	{
		label = GetComponent<UILabel> ();
	}

	private IEnumerator UpdateText ()
	{
		var player = GameManager.instance.GetCurrentPlayer ();
		var lastPlayer = player;
		label.text = "Jugador " + player.playerIndex.ToString ();

		while (true)
		{
			yield return null;

			if (player != lastPlayer)
			{
				if (player != null)
					label.text = "Jugador " + player.playerIndex.ToString ();

				lastPlayer = player;
			}
			player = GameManager.instance.GetCurrentPlayer ();
		}
	}

	private void OnEnable ()
	{
		StartCoroutine (UpdateText ());
	}
}
