using UnityEngine;
using RPG.Movement;
using RPG.Core;
using RPG.Saving;
using RPG.Resources;

namespace RPG.Combat
{
    [SelectionBase]

    public class Fighter : MonoBehaviour, IAction, ISaveable
    {
        [SerializeField] float timeBetweenAttacks = 1f;
        [SerializeField] Transform rightHandTransform = null;
        [SerializeField] Transform leftHandTransform = null;
        [SerializeField] Weapon defaultWeapon = null;

        private Health target = null;
        Weapon currentWeapon = null;

        private float timeSinceLastAttack = Mathf.Infinity;

        void Start()
        {
            if (currentWeapon == null)
            {
                EquipWeapon(defaultWeapon);
            }
        }

        public void EquipWeapon(Weapon weapon)
        {
            if (weapon == null) { Debug.LogError("Weapon is null"); ; }
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public Health GetTarget()
        {
            return target;
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
                currentWeapon.LaunchProjectille(rightHandTransform, leftHandTransform, target, gameObject);
            else
                target.TakeDamage(gameObject, currentWeapon.GetWeaponDamage());
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

        public object CaptureState()
        {
            return currentWeapon.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;

            Weapon weapon = UnityEngine.Resources.Load<Weapon>(weaponName);

            EquipWeapon(weapon);
        }
    }
}