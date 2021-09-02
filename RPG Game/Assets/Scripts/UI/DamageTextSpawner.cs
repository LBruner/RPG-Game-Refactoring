using UnityEngine;

namespace GameDevTV.UI
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] DamageText damageTextPrefab = null;

        public void SpawnDamageText(float damage)
        {
            DamageText instance = Instantiate<DamageText>(damageTextPrefab, transform);

            instance.SetText(damage);
        }
    }
}
