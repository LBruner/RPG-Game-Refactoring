using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] float weaponDamage = 5f;

        private Transform target = null;

        private float timeSinceLastAttack;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                AttackBehavior();
                GetComponent<Mover>().Cancel();
            }
        }

        private void AttackBehavior()
        {
            if (timeSinceLastAttack >= timeBetweenAttacks)
            {
                GetComponent<Animator>().SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        //animation event

        private void Hit()
        {
            if (target != null)
            {
                target.TryGetComponent<Health>(out Health targetHealth);
                targetHealth.TakeDamage(weaponDamage);
            }
        }

        private bool GetIsInRange()
        {
            return (target.transform.position - transform.position).sqrMagnitude < weaponRange * weaponRange;
        }

        public void Attack(CombatTarget combatTarget)
        {
            FindObjectOfType<ActionScheduler>().StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
        }



    }
}