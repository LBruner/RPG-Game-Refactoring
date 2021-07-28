using UnityEngine;

namespace RPG.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        MonoBehaviour lastAction = null;

        public void StartAction(MonoBehaviour action)
        {
            if (lastAction == action) return;

            if (lastAction != null)
            {
                Debug.Log("Cancelling" + lastAction);
            }
            lastAction = action;
        }
    }
}