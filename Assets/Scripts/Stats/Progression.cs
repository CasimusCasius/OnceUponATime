using System.Collections.Generic;
using UnityEngine;

namespace Game.Stats
{
    [CreateAssetMenu(fileName = "Progression", menuName = "Stats/New Progression", order = 1)]
    public class Progression : ScriptableObject
    {
        [SerializeField] ProgressionCharacterClass[] characterClass;
        Dictionary<ECharacterClass, Dictionary<EStat, float[]>> lookupTable = null;

        public float GetStat(EStat stat, ECharacterClass characterClass, int level)
        {
            BuildLookup();

            float[] levels = lookupTable[characterClass][stat];

            if (levels.Length < level - 1)
            {
                return 0;
            }

            return levels[level - 1];
        }

        public int GetNumberOfProgressionLevels(EStat stat, ECharacterClass characterClass)
        {
            BuildLookup();
            return lookupTable[characterClass][stat].Length;
        }

        private void BuildLookup()
        {
            if (lookupTable != null) return;

            lookupTable = new Dictionary<ECharacterClass, Dictionary<EStat, float[]>>();

            foreach (var character in characterClass)
            {
                Dictionary<EStat, float[]> statsTable = new Dictionary<EStat, float[]>();
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
            public ECharacterClass characterClass;
            public ProgressionStat[] stats;
        }

        [System.Serializable]
        class ProgressionStat
        {
            public EStat stat;
            public float[] levels;
        }
    }
}