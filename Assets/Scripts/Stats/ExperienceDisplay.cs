using System;
using TMPro;
using UnityEngine;

namespace Game.Stats
{
    public class ExperienceDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI experienceText;
        Experience experience = null;

        private void Awake()
        {
            experience = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        void Update()
        {
            experienceText.text = String.Format($"{experience.GetCurrentExperience():0}");
        }
    }
}