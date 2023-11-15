using Game.Attributes;
using UnityEngine;

namespace Game.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isHoming = false;
        [SerializeField] GameObject hitEffect = null;
        [SerializeField] float maxLifeTime = 5f;
        [SerializeField] GameObject[] destroyOnHit = null;
        [SerializeField] float lifetimeAfterImpact = 2f;
        Health target = null;
        float damage = 0;
        GameObject instigator = null;

        private void Start()
        {
            transform.LookAt(target.transform.position);
            Destroy(gameObject, maxLifeTime);
        }
     
        void Update()
        {
            if (target == null) return;
            if (isHoming && target.IsAlive())
            {
                transform.LookAt(target.transform.position);
            }
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, GameObject instigator, float damage)
        {
            this.target = target;
            this.damage = damage;
            this.instigator = instigator;
        }

        private void OnTriggerEnter(Collider other)
        {
            var candidateTarget = other.GetComponent<Health>();
            if (candidateTarget != null && candidateTarget == target && candidateTarget.IsAlive())
            {

                target.TakeDamage(instigator, damage);
                speed = 0;
                if (hitEffect != null)
                {
                    Instantiate(hitEffect, target.transform.position, transform.rotation);
                }

                foreach (var toDestroy in destroyOnHit)
                {
                    Destroy(toDestroy);
                }

                Destroy(gameObject, lifetimeAfterImpact);
            }

        }
    }
}
