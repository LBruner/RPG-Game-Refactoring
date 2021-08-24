using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class HealthDisplay : MonoBehaviour
    {
        Health health = null;
        Text displayText = null;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
            displayText = GetComponent<Text>();
        }

        private void Update()
        {
            displayText.text = String.Format("{0:0}/{1:0}", health.GetHealthPoints(), health.GetMaxHealthPoints());
        }
    }
}
