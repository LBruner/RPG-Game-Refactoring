using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI AIText;
        PlayerConversant playerConversant;

        private void Start()
        {
            playerConversant = GameObject.FindWithTag("Player").GetComponent<PlayerConversant>();

            AIText.text = playerConversant.GetText();
        }
    }
}
