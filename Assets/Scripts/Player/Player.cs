using System;

namespace Menu.Characters
{
	public class Player
	{
		public Characters character;
		public readonly int playerIndex;

		public Player (Characters character, int playerIndex)
		{
			this.character = character;
			this.playerIndex = playerIndex;
		}
	}
}

