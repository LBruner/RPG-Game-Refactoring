using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Stats
{
    public class BaseStats : MonoBehaviour
    {
        [Range(1, 99)]
        [SerializeField] int startingLevel = 1;
        [SerializeField] CharacterClass characterClass;
        [SerializeField] Progression progression = null;
        [SerializeField] GameObject levelUpVFX = null;
        [SerializeField] bool shouldUseModifiers = false;
        public Action onLevelUp;

        int currentLevel = 0;

        private void Start()
        {
            currentLevel = CalculateLevel();

            Experience experience = GetComponent<Experience>();

            if (experience != null)
            {
                experience.onExperienceGained += UpdateLevel;
            }
        }

        private void UpdateLevel()
        {
            int newLevel = CalculateLevel();

            if (newLevel > currentLevel)
            {
                currentLevel = newLevel;

                onLevelUp?.Invoke();

                LevelUpEffect();
            }
        }

        private void LevelUpEffect()
        {
            Instantiate(levelUpVFX, transform);
        }

        public float GetStat(Stat stat)
        {
            return (GetBaseStat(stat) + GetAdditiveModifiers(stat)) * (1 + GetPercentageModifier(stat) / 100);
        }

        private float GetBaseStat(Stat stat)
        {
            return progression.GetStat(stat, characterClass, GetLevel());
        }

        public int GetLevel()
        {
            if (currentLevel < 1)
                currentLevel = CalculateLevel();

            return currentLevel;
        }

        private float GetAdditiveModifiers(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (var modifier in provider.GetAdditiveModifiers(stat))
                {
                    total += modifier;
                }
                return total;
            }
            return total;
        }

        private float GetPercentageModifier(Stat stat)
        {
            if (!shouldUseModifiers) return 0;

            float total = 0;
            foreach (var provider in GetComponents<IModifierProvider>())
            {
                foreach (var modifier in provider.GetPercentageModifiers(stat))
                {
                    total += modifier;
                }
                return total;
            }
            Debug.Log(total);
            return total;
        }


        private int CalculateLevel()
        {
            Experience experience = GetComponent<Experience>();

            if (experience == null)
                return startingLevel;

            float currentXP = experience.GetExperiencePoints();

            int penultimateLevel = progression.GetLevels(Stat.ExperienceToLevelUp, characterClass);

            for (int level = 1; level <= penultimateLevel; level++)
            {
                float XPToLevelUp = progression.GetStat(Stat.ExperienceToLevelUp, characterClass, level);

                if (XPToLevelUp > currentXP)
                {
                    return level;
                }
            }

            return penultimateLevel + 1;
        }
    }
}

