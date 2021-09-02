using TMPro;
using UnityEngine;

namespace GameDevTV.UI
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI damageText = null;

        public void SetText(float damage)
        {
            damageText.text = string.Format("{0}", damage);
        }
    }
}
