using System;
using UnityEngine;

namespace RPG.Attributes
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] RectTransform foreground = null;
        [SerializeField] Health health = null;

        private void Update()
        {
            foreground.localScale = new Vector3(health.GetFraction(), 1, 1);
        }
    }
}