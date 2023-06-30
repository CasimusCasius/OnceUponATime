using Game.Core;
using Game.Movement;
using UnityEngine;

namespace Game.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange = 3f;
        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private float timeBetweenAttacks = 1.5f;

        private Transform target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;
            if (!GetIsInRange())
            {
                GetComponent<Mover>()?.MoveTo(target.position);
            }
            else
            {
                GetComponent<Mover>()?.Cancel();

                AttackBehaviour();
            }
        }



        public void Attack(CombatTarget combatTarget)
        {
            GetComponent<ActionScheduler>()?.StartAction(this);
            target = combatTarget.transform;
        }

        public void Cancel()
        {
            target = null;
            Debug.Log(this);
        }
        // animation Event
        public void Hit()
        {
            if (target.TryGetComponent(out Health targetHealth))
            {
                targetHealth.TakeDamage(weaponDamage);
            }
        }
        private void AttackBehaviour()
        {
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                GetComponent<Animator>()?.SetTrigger("attack");
                timeSinceLastAttack = 0;
            }
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.position, transform.position) <= weaponRange;
        }



    }
}