using GameDevTV.Control;
using GameDevTV.Core;
using UnityEngine;
using UnityEngine.Playables;

public class CinematicControlRemover : MonoBehaviour
{
    GameObject player;
    PlayableDirector director;
    private void Awake()
    {
        player = GameObject.FindWithTag("Player");
        director = GetComponent<PlayableDirector>();
    }

    private void OnEnable()
    {
        director.played += EnableControl;
        director.stopped += DisableControl;
    }

    private void OnDisable()
    {
        director.played -= EnableControl;
        director.stopped -= DisableControl;
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
