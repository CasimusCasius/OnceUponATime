using System;
using UnityEngine;

namespace Game.Stats
{
    public class BaseStats : MonoBehaviour
    {
        public event Action onLevelUp;

        [SerializeField][Range(1, 99)] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpParticleEffect;
        [SerializeField] bool shouldUseModifiers = false;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = GetLevel();
            if (TryGetComponent<Experience>(out var experience))
            {
                experience.onExpirienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();
            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;
                LevelUpEffect();
                onLevelUp();

            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpParticleEffect, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifier(stat)) * (1 + GetPercentageModifier(stat)/100);
        }



        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
            {
                currentLevel = CalculateLevel();
            }
            return currentLevel;
        }

        private float GetAdditiveModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float total = 0;
            IModifierProvider[] providers = GetComponents<IModifierProvider>();
            foreach (IModifierProvider provider in providers)
            {
                foreach (float modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;

        }
        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;
            float total = 0;
            IModifierProvider[] providers = GetComponents<IModifierProvider>();
            foreach (IModifierProvider provider in providers)
            {
                foreach (float modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
            }
            return total;
        }
        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();
            if (experience == null) return startingLevel;
            float currentXP = experience.GetCurrentExperience();

            int penultimateLevel = progression.GetNumberOfProgressionLevels(Stat.PointsToLevelUp,
                characterClass);

            for (int i = 1; i <= penultimateLevel; i++)
            {
                if (progression.GetStat(Stat.PointsToLevelUp, characterClass, i) > currentXP)
                    return i;
            }
            return penultimateLevel + 1;
        }
    }
}
