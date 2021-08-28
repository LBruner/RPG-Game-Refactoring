using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;

        public void SpawnDamageText(float damage)
        {
            Debug.Log(gameObject.name);
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);

            instance.SetText(damage);
        }
    }
}
