using RPG.Resources;
using UnityEngine;

namespace RPG.Combat
{
    public class Projectille : MonoBehaviour
    {
        [SerializeField] float speed = 5f;
        [SerializeField] bool isSeeker = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 10f;
        [SerializeField] GameObject[] destroyOnHit;
        [SerializeField] float lifeAfterImpact = 2f;
        float damage = 0;
        Health target = null;
        GameObject instigator = null;

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

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.instigator = instigator;
            this.damage = damage;

            Destroy(gameObject, maxLifeTime);
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

            target.TakeDamage(instigator, damage);

            speed = 0;

            if (hitEffect != null)
                Instantiate(hitEffect, GetAimLocation(), transform.rotation);

            foreach (GameObject toDestroy in destroyOnHit)
            {
                Destroy(toDestroy);
            }

            Destroy(gameObject, lifeAfterImpact);
        }
    }
}