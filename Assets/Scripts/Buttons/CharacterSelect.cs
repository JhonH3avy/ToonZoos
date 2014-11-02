using UnityEngine;
using System.Collections;
using Menu.Characters;

[RequireComponent (typeof (UISprite), typeof (UIButtonScale))]
public class CharacterSelect : MonoBehaviour
{
	[SerializeField] private Characters _characterRepresentation;
	[SerializeField] private UISprite _spriteRender;
	[SerializeField] private UIButtonScale _button;

	private void Awake ()
	{
		_spriteRender = GetComponent<UISprite>();
		_button = GetComponent<UIButtonScale>();
	}

	public void Select ()
	{
		if (_button.enabled)
			GameManager.instance.PlayerSelection (_characterRepresentation);

		_spriteRender.color = Color.red;
		_button.enabled = false;
	}

	private void OnEnable ()
	{
		if (!_button.enabled)
			_button.enabled = true;
		if (_spriteRender.color != Color.white)
			_spriteRender.color = Color.white;
	}
}
