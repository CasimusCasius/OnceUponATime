using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Game.Cinematics
{
	public class CinematicTrigger : MonoBehaviour
	{
        [SerializeField]bool wasPlayed = false;
        private void OnTriggerEnter(Collider other)
        {

            if (other.gameObject.CompareTag("Player") && !wasPlayed)
            {
                GetComponent<PlayableDirector>().Play();
                
                wasPlayed = true;
            }
        }



    } 
}
