using UnityEngine;
using System.Collections;

public class ObstacleStone : ObstacleController
{
	[SerializeField]
	private float _slowFactor = 0.5f;

	#region implemented abstract members of ObstacleController
	// FIXME El efecto del obstaculo hace retoreceder al personaje si no esta saltando
	protected override void ObstacleEffect (GameObject player)
	{
		Debug.Log ("Toco la pieda");
		AnimatorStateInfo playerAnimInfo = player.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);

		if (!playerAnimInfo.IsName ("Base.Jump")
		    && (playerAnimInfo.normalizedTime + float.Epsilon + Time.deltaTime) > 0.8f)
		{
			player.SendMessage ("SlowDown", _slowFactor);
		}
	}

	#endregion
}
