using Game.Attributes;
using Game.Combat;
using Game.Movement;
using UnityEngine;


namespace Game.Control
{

    public class PlayerController : MonoBehaviour
    {

        Health myHealth;

        private void Awake()
        {
            myHealth = GetComponent<Health>();
        }

        void Update()
        {
            if (myHealth == null || !myHealth.IsAlive()) return;
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            //print("Nothing happend");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                if (!hit.transform.TryGetComponent<CombatTarget>(out var target)) continue;

                if (!GetComponent<Fighter>().CanAttack(target.gameObject))
                {
                    continue;
                }

                if (Input.GetMouseButton(0))
                {
                    GetComponent<Fighter>().Attack(target.gameObject);
                }
                return true;  // even on mouse hoover
            }
            return false;
        }

        private bool InteractWithMovement()
        {

            if (Physics.Raycast(GetMouseRay(), out RaycastHit hit))
            {
                if (Input.GetMouseButton(0))
                {

                    GetComponent<Mover>().StartMoveAction(hit.point);

                }
                return true;
            }
            return false;
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
