using UnityEngine;

namespace RPG.Core
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] Transform targetPos = null;

        private void LateUpdate()
        {
            transform.position = targetPos.position;
        }
    }
}