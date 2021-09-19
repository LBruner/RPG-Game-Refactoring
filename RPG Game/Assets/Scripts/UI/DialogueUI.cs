using System.Collections;
using System.Collections.Generic;
using Dialogue;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace RPG.UI
{
    public class DialogueUI : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI AIText;
        PlayerConversant playerConversant;
        [SerializeField] Button nextButton;


        private void Start()
        {
            playerConversant = GameObject.FindWithTag("Player").GetComponent<PlayerConversant>();

            nextButton.onClick.AddListener(Next);

            UpdateUI();
        }

        private void UpdateUI()
        {
            AIText.text = playerConversant.GetText();
            nextButton.gameObject.SetActive(playerConversant.HasNext());
        }

        private void Next()
        {
            playerConversant.Next();
            UpdateUI();
        }
    }
}
