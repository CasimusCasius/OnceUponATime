using Game.Movement;
using UnityEngine;
using Game.Core;

namespace Game.Combat
{
    public class Fighter : MonoBehaviour
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
                GetComponent<Mover>()?.Stop();
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
        }

        private bool GetIsInRange()
        {
            return Vector3.Distance(target.position, transform.position) <= weaponRange;
        }
    }
}