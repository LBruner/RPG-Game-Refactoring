using System.Collections;
using System.Collections.Generic;
using RPG.Combat;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        Fighter player = null;

        Text displayText = null;

        private void Start()
        {
            player = GameObject.FindWithTag("Player").GetComponent<Fighter>();
            displayText = GetComponent<Text>();
        }

        private void Update()
        {
            if (!player.GetTarget())
                displayText.text = string.Format("{0}", "NA");
            else
                displayText.text = string.Format("{0}%", player.GetTarget().GetComponent<Health>().GetPercentage());
        }
    }
}
