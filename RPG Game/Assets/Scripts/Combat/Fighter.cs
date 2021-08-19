using UnityEngine;
using RPG.Movement;
using RPG.Core;
using System;

namespace RPG.Combat
{
    [SelectionBase]

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;
        [SerializeField] string defaulWeaponName = "Unarmed";

        private Health target = null;
        Weapon currentWeapon = null;

        private float timeSinceLastAttack = Mathf.Infinity;

        void Start()
        {
            Weapon weapon = Resources.Load<Weapon>(defaulWeaponName);

            EquipWeapon(weapon);
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == null) { Debug.LogError("Weapon is null"); ; }
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) { return; }
            if (target.GetIsDead()) { return; }

            if (!GetIsInRange())
            {
                GetComponent<Mover>().MoveTo(target.transform.position);
            }
            else
            {
                GetComponent<Mover>().Cancel();
                AttackBehavior();
            }
        }

        private void AttackBehavior()
        {
            transform.LookAt(target.transform);

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>().ResetTrigger("cancelAttack");
            GetComponent<Animator>().SetTrigger("attack");
        }

        //animation event

        private void Hit()
        {
            if (target == null) { return; }


            if (currentWeapon.HasProjectille())
                currentWeapon.LaunchProjectille(rightHandTransform, leftHandTransform, target);
            else
                target.TakeDamage(currentWeapon.GetWeaponDamage());
        }

        private void Shoot()
        {
            Hit();
        }

        private bool GetIsInRange()
        {
            if (defaultWeapon != null)
                return Vector3.Distance(transform.position, target.transform.position) < currentWeapon.GetWeaponRange();
            else
                return false;
        }

        public bool CanAttack(GameObject combatTarget)
        {
            if (combatTarget == null)
            {
                return false;
            }

            Health targetToTest = combatTarget.GetComponent<Health>();

            return targetToTest != null && !targetToTest.GetIsDead();
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {
            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            GetComponent<Animator>().ResetTrigger("attack");
            GetComponent<Animator>().SetTrigger("cancelAttack");
        }
    }
}