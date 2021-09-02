using UnityEngine;
using UnityEngine.UI;

namespace GameDevTV.Stats
{
    public class LevelDisplay : MonoBehaviour
    {
        Text displayText = null;
        BaseStats player = null;

        private void Awake()
        {
            displayText = GetComponent<Text>();
            player = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }

        private void Update()
        {
            displayText.text = string.Format("{0}", player.GetLevel());
        }
    }
}
