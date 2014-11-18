using UnityEngine;
using System.Collections;

namespace Menu.Button
{	
	public delegate void ButtonEventHandler ();

	public class Button : MonoBehaviour
	{
		public event ButtonEventHandler Clicking;

		private void OnMouseUpAsButton ()
		{
			OnClicking ();
		}

		private void OnClicking ()
		{
			ButtonEventHandler handler = Clicking;

			if (handler != null)
			{
				handler.Invoke ();
			}
		}
	}
}