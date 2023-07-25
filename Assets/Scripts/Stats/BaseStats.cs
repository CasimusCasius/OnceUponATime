using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField][Range(1, 99)] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetHealth()
        {
            return progression.GetHealth(characterClass,startingLevel);
        }

    }
}
