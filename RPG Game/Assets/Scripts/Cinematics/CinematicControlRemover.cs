using System.Collections;
using System.Collections.Generic;
using RPG.Control;
using RPG.Core;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    GameObject player;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");

        PlayableDirector director = GetComponent<PlayableDirector>();

        director.played += EnableControl;
        director.stopped += DisableControl;
    }

    void EnableControl(PlayableDirector director)
    {
        player.GetComponent<ActionScheduler>().CancelCurrentAction();
        player.GetComponent<PlayerController>().enabled = false;
    }

    void DisableControl(PlayableDirector director)
    {
        player.GetComponent<PlayerController>().enabled = true;
    }
}
