using UnityEngine;

namespace RPG.Combat
{

    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make new weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] AnimatorOverrideController weaponOverride = null;
        [SerializeField] GameObject equippedPrefab = null;
        [SerializeField] float weaponRange = 2f;
        [SerializeField] float weaponDamage = 5f;

        public void Spawn(Transform handTransform, Animator animator)
        {
            if (equippedPrefab == null) { return; }
            Instantiate(equippedPrefab, handTransform);

            if (animator == null) { return; }
            animator.runtimeAnimatorController = weaponOverride;
        }

        public float GetWeaponRange()
        {
            return weaponRange;
        }

        public float GetWeaponDamage()
        {
            return weaponDamage;
        }
    }
}