using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stats
{

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass;

        public float GetHealth(CharacterClass characterClass, int level)
        {
            foreach (var character in this.characterClass)
            {
                if (character.characterClass != characterClass) continue;
                return character.health[level-1];
            }
            return 0;
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public float[] health ;
        }
    }

}