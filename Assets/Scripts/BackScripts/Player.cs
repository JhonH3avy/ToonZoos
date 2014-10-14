using System;

namespace Menu.Characters
{
	public class Player
	{
		public Characters character;
		public readonly int playerIndex;

		public Player (Characters _character, int index)
		{
			character = _character;
			playerIndex = index;
		}
	}
}

