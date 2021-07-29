using UnityEngine;
using RPG.Movement;
using RPG.Core;

namespace RPG.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float weaponRange = 2f;

        private Transform target = null;

        private void Update()
        {
            Debug.Log(target);
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
            GetComponent<Animator>().SetTrigger("attack");
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


        //animation event

        private void Hit()
        {

        }
    }
}