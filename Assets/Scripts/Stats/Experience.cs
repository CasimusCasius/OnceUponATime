using Game.Saving;
using System;
using System.Collections;
using UnityEngine;

namespace Game.Stats
{ 
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoint = 0f;

        public event Action onExpirienceGained;

        public void GainExperience(float experience)
        {
            experiencePoint += experience;
            onExpirienceGained();
        }

        public float GetCurrentExperience() => experiencePoint;

        public object CaptureState()
        {
            return experiencePoint;
        }

        public void RestoreState(object state)
        {
            experiencePoint = (float) state;
        }
    }
}