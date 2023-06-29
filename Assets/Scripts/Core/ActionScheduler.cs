using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
	public class ActionScheduler : MonoBehaviour
	{
        MonoBehaviour previousAction;
        public void StartAction(MonoBehaviour action)
        {
            if (previousAction == action) return;
            if (previousAction != null)
            {
                print(previousAction + " Canceled");
            }
            previousAction = action;
        }
    }

}