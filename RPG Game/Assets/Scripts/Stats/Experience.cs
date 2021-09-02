using System;
using GameDevTV.Saving;
using UnityEngine;

namespace GameDevTV.Stats
{
    public class Experience : MonoBehaviour, ISaveable
    {
        [SerializeField] float experiencePoints = 0f;

        public Action onExperienceGained;

        public float GetExperiencePoints()
        {
            return experiencePoints;
        }

        public void GainExperience(float experience)
        {
            experiencePoints += experience;
            onExperienceGained?.Invoke();
        }

        public object CaptureState()
        {
            return experiencePoints;
        }


        public void RestoreState(object state)
        {
            float XP = (float)state;

            experiencePoints = XP;
        }
    }
}