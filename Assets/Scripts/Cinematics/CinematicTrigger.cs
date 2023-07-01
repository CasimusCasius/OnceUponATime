using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Game.Cinematics
{
	public class CinematicTrigger : MonoBehaviour
	{
        bool wasPlayed = false;
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.tag == "Player" && !wasPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                other.gameObject.GetComponent<ActionScheduler>().CancelCurrentAction();
                wasPlayed = true;
            }
        }



    } 
}
