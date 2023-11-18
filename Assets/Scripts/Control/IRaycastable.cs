using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
	public interface IRaycastable
	{
		bool HandleRaycast(PlayerController callingController);
	}

}