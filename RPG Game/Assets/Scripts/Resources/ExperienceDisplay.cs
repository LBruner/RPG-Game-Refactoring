using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.Resources
{
    public class ExperienceDisplay : MonoBehaviour
    {
        Text displayText = null;
        Experience player = null;

        private void Awake()
        {
            displayText = GetComponent<Text>();
            player = GameObject.FindWithTag("Player").GetComponent<Experience>();
        }

        private void Update()
        {
            displayText.text = string.Format("{0}", player.GetExperiencePoints());
        }
    }
}
