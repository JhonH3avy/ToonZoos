using UnityEngine;
using System.Collections;

namespace Menu.Button
{	
	public delegate void ButtonDelegate ();

	public class Button : MonoBehaviour
	{
		public event ButtonDelegate Click;

		private void OnMouseUpAsButton ()
		{
			OnClick ();
		}

		private void OnClick ()
		{
			ButtonDelegate handler = Click;

			if (handler != null)
			{
				handler.Invoke ();
			}
		}
	}
}