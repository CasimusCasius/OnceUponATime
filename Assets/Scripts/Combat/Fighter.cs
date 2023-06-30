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

        private Health target;
        float timeSinceLastAttack = Mathf.Infinity;

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!target.IsAlive())return; 

            if (!GetIsInRange())
            {
                GetComponent<Mover>()?.MoveTo(target.transform.position);
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
            target = combatTarget.GetComponent<Health>();
           
        }

        public void Cancel()
        {
            StopAttack();
            target = null;

        }

        private void StopAttack()
        {
            GetComponent<Animator>()?.ResetTrigger("attack");
            GetComponent<Animator>()?.SetTrigger("outAttack");
        }

        // animation Event
        public void Hit()
        { 
            target?.TakeDamage(weaponDamage);

        }
        private void AttackBehaviour()
        {
            transform.LookAt(target.transform);

            if (target !=null && !target.IsAlive()) Cancel();
            
            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            GetComponent<Animator>()?.ResetTrigger("stopAttack");
            GetComponent<Animator>()?.SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.transform. position, transform.position) <= weaponRange;
        }

        public bool CanAttack(CombatTarget combatTarget)
        {
            return (combatTarget != null && combatTarget.GetComponent<Health>().IsAlive());
            
        }


    }
}