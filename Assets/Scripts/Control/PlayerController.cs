using Game.Combat;
using Game.Movement;
using UnityEngine;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {
            if (InteractWithCombat()) return;
            if (InteractWithMovement()) return;
            print("Nothing happend");
        }

        private bool InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null || !GetComponent<Fighter>().CanAttack(target))
                {
                   
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>()?.Attack(target);
                }
                return true;  // even on mouse hoover
            }
            return false;
        }

        private bool InteractWithMovement()
        {

            if (Physics.Raycast(GetMouseRay(),out RaycastHit hit))
            {
                if (Input.GetMouseButton(0))
                {
                    
                    GetComponent<Mover>()?.StartMoveAction(hit.point);

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
