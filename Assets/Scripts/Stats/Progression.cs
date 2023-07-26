using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Stats
{

    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass;
        Dictionary<CharacterClass, Dictionary<Stat, float[]>> lookupTable = null;


        public float GetStat(Stat stat, CharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels =  lookupTable[characterClass][stat];

            if (levels.Length < level-1)
            {
                return 0;
            }

            return levels[level - 1];
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<CharacterClass, Dictionary<Stat, float[]>>();

            foreach (var character in characterClass)
            {
                Dictionary<Stat, float[]> statsTable = new Dictionary<Stat, float[]>();
                foreach (var progressionStat in character.stats)
                {
                    statsTable[progressionStat.stat] = progressionStat.levels;
                }

                lookupTable[character.characterClass] = statsTable;
            }
        }

        [System.Serializable]
        class ProgressionCharacterClass
        {
            public CharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public Stat stat;
            public float[] levels;

        }

    }

}