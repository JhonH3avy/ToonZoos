using UnityEngine;
using UnityEngine.UI;
using Scenes;
using System.Collections;

public class SceneChangeButton : MonoBehaviour
{
	public Scene scene;
	public bool makeInteractable;

	private void Start ()
	{
		if(makeInteractable == true)
		{
			MenuTrasition.TransitionEnded += MakeInteractable;
		}
	}

	public void ChangeScene ()
	{
		Application.LoadLevel ((int)scene);
	}

	private void MakeInteractable ()
	{
		Button button = GetComponent<Button> ();
		button.interactable = true;
	}

}
