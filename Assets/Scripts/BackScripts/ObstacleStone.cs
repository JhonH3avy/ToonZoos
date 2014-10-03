using UnityEngine;
using System.Collections;

public class ObstacleStone : ObstacleController
{
	#region implemented abstract members of ObstacleController

	protected override void ObstacleEffect (GameObject player)
	{
		AnimatorStateInfo playerAnimInfo = player.GetComponent<Animator> ().GetCurrentAnimatorStateInfo (0);

		if (!playerAnimInfo.IsName ("Base.Jump")
		    && (playerAnimInfo.normalizedTime + float.Epsilon + Time.deltaTime) > 0.8f)
		{
			player.SendMessage ("Fall");
		}
	}

	#endregion
}
