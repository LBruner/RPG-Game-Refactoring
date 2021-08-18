using System;
using System.Collections;
using System.Collections.Generic;
using RPG.Core;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectille : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] bool isSeeker = false;
        [SerializeField] GameObject hitEffect = null;

        float damage = 0;
        Health target = null;

        private void Start()
        {
            transform.LookAt(GetAimLocation());
        }

        private void Update()
        {
            if (target != null && isSeeker)
            {
                if (!target.GetIsDead())
                    transform.LookAt(GetAimLocation());
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private Vector3 GetAimLocation()
        {
            CapsuleCollider targetCapsule = target.GetComponent<CapsuleCollider>();

            if (targetCapsule == null)
            {
                return target.transform.position;
            }

            return target.transform.position + Vector3.up * targetCapsule.height / 2;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<Health>() != target || other.GetComponent<Health>().GetIsDead()) { return; }

            target.TakeDamage(damage);

            if (hitEffect != null)
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);

            Destroy(gameObject);
        }
    }
}