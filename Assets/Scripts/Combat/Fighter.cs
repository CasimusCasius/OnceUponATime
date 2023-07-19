using Game.Core;
using Game.Movement;
using UnityEngine;

namespace Game.Combat
{

    public class Fighter : MonoBehaviour, IAction
    {
        [SerializeField] private float timeBetweenAttacks = 1.5f;
        [SerializeField] private Transform handTransform = null;
        [SerializeField] private Weapon defaultWeapon = null;
        
        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;
        private Weapon currentWeapon;

        private void Start()
        {
            currentWeapon = defaultWeapon;
            EquipWeapon(defaultWeapon);

        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!target.IsAlive()) return;

            if (TryGetComponent(out Mover mover))
            {
                if (!GetIsInRange())
                {

                    mover.MoveTo(target.transform.position);

                }
                else
                {
                    mover.Cancel();

                    AttackBehaviour();
                }
            }
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {

            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        private void StopAttack()
        {
            if (TryGetComponent(out Animator animator))
                animator.ResetTrigger("attack");
            animator.SetTrigger("outAttack");
        }

        // animation Event
        public void Hit()
        {
            if (target == null) return;
            target.TakeDamage(currentWeapon.GetWeaponDamage());

        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon = weapon;
            Animator animator = GetComponent<Animator>();
            currentWeapon.Spawn(handTransform, animator);
        }

        private void AttackBehaviour()
        {

            transform.LookAt(target.transform);

            if (target != null && !target.IsAlive()) Cancel();

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            if (TryGetComponent(out Animator animator))
            {
                animator.ResetTrigger("outAttack");
                animator.SetTrigger("attack");
            }
        }

        private bool GetIsInRange()
        {
            return 
                Vector3.Distance(target.transform.position, transform.position) <= currentWeapon.GetWeaponRange();
        }

        public bool CanAttack(GameObject combatTarget)
        {
            return (combatTarget != null && combatTarget.GetComponent<Health>().IsAlive());

        }


    }
}