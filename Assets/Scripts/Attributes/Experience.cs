using Game.Saving;
using System.Collections;
using UnityEngine;

namespace Game.Attributes
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoint = 0f;

        public void GainExperience(float experience)
        {
            experiencePoint += experience;
        }

        public float GetExperience() => experiencePoint;
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