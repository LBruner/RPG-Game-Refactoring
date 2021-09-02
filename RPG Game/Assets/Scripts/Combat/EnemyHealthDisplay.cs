using System.Collections;
using System.Collections.Generic;
using GameDevTV.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace GameDevTV.Attributes
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter player = null;

        Text displayText = null;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            displayText = GetComponent<Text>();
        }

        private void Update()
        {
            if (!player.GetTarget())
                displayText.text = string.Format("{0}", "NA");
            else
            {
                Health enemy = player.GetTarget().GetComponent<Health>();
                displayText.text = string.Format("{0:0}/{1:0}", enemy.GetHealthPoints(), enemy.GetMaxHealthPoints());
            }
        }
    }
}
