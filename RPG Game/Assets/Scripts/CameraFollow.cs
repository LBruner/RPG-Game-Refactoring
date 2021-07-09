using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform targetPos = null;

    private void Update()
    {
        transform.position = targetPos.position;
    }
}