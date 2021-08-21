using UnityEngine;

public class Experience : MonoBehaviour
{
    [SerializeField] float expiriencePoints = 0f;

    public void GainExperience(float experience)
    {
        expiriencePoints += experience;
    }
}