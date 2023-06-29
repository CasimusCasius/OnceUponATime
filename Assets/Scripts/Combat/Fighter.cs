using Game.Movement;
using UnityEngine;
using Game.Core;

namespace Game.Combat
{
    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float weaponRange;

        private Transform target;

        private void Update()
        {
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

        private void AttackBehaviour()
        {
            GetComponent<Animator>()?.SetTrigger("attack");
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.position, transform.position) <= weaponRange;
        }

        // animation Event
        void Hit()
        {

        }

    }
}