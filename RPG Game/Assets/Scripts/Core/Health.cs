using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.Core
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float health = 100f;

        bool isDead;

        public void TakeDamage(float damage)
        {
            health = Mathf.Max(health - damage, 0);

            if (health == 0)
            {
                Die();
            }
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
            return health;
        }

        public void RestoreState(object state)
        {
            float savedHealth = (float)state;
            health = savedHealth;

            if (health == 0)
            {
                Die();
            }
        }
    }
}
