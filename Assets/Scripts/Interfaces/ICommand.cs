using System;

namespace Scripts.Interfaces
{
	
	public delegate void ButtonPressEventHandler ();

	public interface ICommand
	{
		event ButtonPressEventHandler ButtonPress;

		void Jump ();
		void Ability ();
		void TrackUp ();
		void TrackDown ();
	}
}

