using Game.Control;
using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

namespace Game.Cinematics
{
    public class CinematicsControlRemover : MonoBehaviour
    {
        GameObject player;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void OnEnable()
        {
            GetComponent<PlayableDirector>().played += DisableControl;
            GetComponent<PlayableDirector>().stopped += EnabledControl;
        }

        private void OnDisable()
        {
            GetComponent<PlayableDirector>().played -= DisableControl;
            GetComponent<PlayableDirector>().stopped -= EnabledControl;
        }
        void DisableControl(PlayableDirector playableDirector)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            player.GetComponent<PlayerController>().enabled = false;
        }

        void EnabledControl(PlayableDirector playableDirector)
        { 
            player.GetComponent<PlayerController>().enabled = true;
        }

    }
}
