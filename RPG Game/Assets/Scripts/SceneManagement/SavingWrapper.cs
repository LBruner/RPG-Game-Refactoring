using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Saving;
using UnityEngine;

namespace RPG.SceneManagement
{
    public class SavingWrapper : MonoBehaviour
    {
        const string defaulSaveFile = "save";

        [SerializeField] float fadeInTime = 0.2f;

        private void Awake()
        {
            StartCoroutine(LoadLastScene());
        }

        IEnumerator LoadLastScene()
        {
            Fader fader = FindObjectOfType<Fader>();
            fader.FadeOutImmediate();

            yield return GetComponent<SavingSystem>().LoadLastScene(defaulSaveFile);
            yield return fader.FadeIn(fadeInTime);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.L))
            {
                Load();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                Save();
            }

            if (Input.GetKeyDown(KeyCode.K))
            {
                DeletSavefile();
            }
        }

        public void Save()
        {
            GetComponent<SavingSystem>().Save(defaulSaveFile);
        }

        public void Load()
        {
            GetComponent<SavingSystem>().Load(defaulSaveFile);
        }

        public void DeletSavefile()
        {
            GetComponent<SavingSystem>().Delete(defaulSaveFile);
        }
    }
}
