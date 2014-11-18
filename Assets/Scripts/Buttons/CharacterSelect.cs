using UnityEngine;
using System.Collections;
using Menu.Characters;

[RequireComponent (typeof (UISprite), typeof (UIButtonScale))]
public class CharacterSelect : MonoBehaviour
{
	[SerializeField] private Characters characterRepresentation;
	[SerializeField] private UISprite spriteRender;
	[SerializeField] private UIButtonScale button;

	private void Awake ()
	{
		spriteRender = GetComponent<UISprite>();
		button = GetComponent<UIButtonScale>();
	}

	public void Select ()
	{
		if (button.enabled)
			GameManager.Instance.PlayerSelection (characterRepresentation);

		spriteRender.color = Color.red;
		button.enabled = false;
	}

	private void OnEnable ()
	{
		if (!button.enabled)
			button.enabled = true;
		if (spriteRender.color != Color.white)
			spriteRender.color = Color.white;
	}
}
