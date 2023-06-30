using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Core
{
	public class ActionScheduler : MonoBehaviour
	{
        IAction previousAction;
        public void StartAction(IAction action)
        {
            if (previousAction == action) return;
            if (previousAction != null)
            {
                previousAction.Cancel();
            }
            previousAction = action;
        }
    }

}