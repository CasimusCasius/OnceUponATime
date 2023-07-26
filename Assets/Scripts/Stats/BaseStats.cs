using UnityEngine;

namespace Game.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [SerializeField][Range(1, 99)] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;

        public float GetStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel(stat,characterClass));
        }

        public int GetLevel(Stat stat, CharacterClass characterClass)
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel; 
            float currentXP = experience.GetCurrentExperience();

            for (int i = 1; i <= progression.GetNumberOfProgressionLevels(stat, characterClass); i++)
            {
                if (progression.GetStat(Stat.PointsToLevelUp, characterClass, i) > currentXP)
                    return i;

            }

            return progression.GetNumberOfProgressionLevels(stat, characterClass) ;
        }


    }
}
