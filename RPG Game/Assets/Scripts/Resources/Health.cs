using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using RPG.Saving;
using RPG.Stats;
using UnityEngine;

namespace RPG.Resources
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float healthPoints = 100f;

        bool isDead;

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            if (healthPoints == 0)
            {
                if (instigator == GameObject.FindWithTag("Player"))
                {
                    AwardExperience(instigator);
                    Die();
                }
            }
        }

        private void AwardExperience(GameObject instigator)
        {
            Experience experience = instigator.GetComponent<Experience>();

            if (experience == null) { return; }

            experience.GainExperience(GetComponent<BaseStats>().GetExperienceReward());
        }


        public float GetPercentage()
        {
            return 100 * (healthPoints / GetComponent<BaseStats>().GetHealth());
        }

        private void Die()
        {
            if (isDead) { return; }

            GetComponent<Animator>().SetTrigger("die");
            GetComponent<ActionScheduler>().CancelCurrentAction();
            isDead = true;
        }

        public bool GetIsDead()
        {
            return isDead;
        }

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            float savedHealth = (float)state;
            healthPoints = savedHealth;

            if (healthPoints == 0)
            {
                Die();
            }
        }
    }
}
