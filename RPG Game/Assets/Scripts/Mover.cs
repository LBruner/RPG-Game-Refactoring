using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Mover : MonoBehaviour
{
    [SerializeField] NavMeshAgent agent = null;
    [SerializeField] Transform target = null;

    Ray lastRay;

    void Update()
    {
        if(Input.GetMouseButtonDown(0))
            MoveToCursor();

    }

    private void MoveToCursor()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        bool hasHit = Physics.Raycast(ray, out hit);

        if(hasHit)
            agent.SetDestination(hit.point);

        Debug.DrawRay(ray.origin, ray.direction * Mathf.Infinity);

    }
}
