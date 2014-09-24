using System;
using UnityEngine;

namespace Scripts.Interfaces
{
	public interface IPlaceable
	{
		void PlaceInTrack (Vector3 initialTrackPosition, int orderInLayer);
	}
}

