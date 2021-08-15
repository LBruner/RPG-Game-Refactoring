using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    private void Start()
    {
        PlayableDirector director = GetComponent<PlayableDirector>();

        director.played += EnableControl;
        director.stopped += EnableControl;
    }

    void EnableControl(PlayableDirector director)
    {
        Debug.Log("E");
    }

    void DisableControl(PlayableDirector director)
    {
        Debug.Log("D");
    }
}
