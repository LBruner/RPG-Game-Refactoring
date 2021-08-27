using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;

        private void Start()
        {
            SpawnDamageText(5);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.U))
                SpawnDamageText(5);
        }

        private void SpawnDamageText(float damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);
        }
    }
}
