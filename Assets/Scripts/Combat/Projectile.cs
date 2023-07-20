using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Health target = null;
        [SerializeField] private float speed = 1;
        float damage = 0;

        // Update is called once per frame
        void Update()
        {
            if (target == null) return;

            transform.LookAt(target.transform.position);
            transform.Translate(Vector3.forward * speed * Time.deltaTime);
        }

        public void SetTarget(Health target, float damage)
        {
            this.target = target;
            this.damage = damage;
        }

        private void OnTriggerEnter(Collider other)
        {
            var candidateTarget = other.GetComponent<Health>();
            if (candidateTarget != null && candidateTarget == target ) 
            {
                target.TakeDamage(damage);

            }
            Destroy(gameObject);

        }
    }
}
