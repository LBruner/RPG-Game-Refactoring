using UnityEngine;

namespace GameDevTV.Core
{
    public class ActionScheduler : MonoBehaviour
    {
        IAction lastAction = null;

        public void StartAction(IAction action)
        {
            if (lastAction == action) return;

            if (lastAction != null)
            {
                lastAction.Cancel();
            }
            lastAction = action;
        }

        public void CancelCurrentAction()
        {
            StartAction(null);
        }
    }
}