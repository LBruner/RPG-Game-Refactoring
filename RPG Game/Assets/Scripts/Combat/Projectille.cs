using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectille : MonoBehaviour
    {
        [SerializeField] Transform target = null;
        [SerializeField] float speed = 5f;

        private void Update()
        {
            if (target == null) { return; }
            transform.LookAt(GetAimLocation());
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
            {
                return target.position;
            }
            return target.position + Vector3.up * targetCapsule.height / 2;
        }
    }
}