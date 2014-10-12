using UnityEngine;
using System.Collections;
using Menu.Characters;

public class CharacterSelect : MonoBehaviour
{
	[SerializeField]
	private Characters _characterRepresentation;

	public void Select ()
	{
		GameManager.instance.PlayerSelection (_characterRepresentation);
	}
}
