using Game.Core;
using UnityEngine;

namespace Game.Combat
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private Health target = null;
        [SerializeField] private float speed = 1;
        [SerializeField] private bool isHoming = false;
        float damage = 0;

        private void Start()
        {
            transform.LookAt(target.transform.position);
            Destroy(gameObject, 5f);
        }
        // Update is called once per frame
        void Update()
        {
            if (target != null && isHoming && target.IsAlive()) return;
            {
                transform.LookAt(target.transform.position);
            }
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
            if (candidateTarget != null && candidateTarget == target && candidateTarget.IsAlive())
            {
                target.TakeDamage(damage);
                Destroy(gameObject);
            }

        }
    }
}
