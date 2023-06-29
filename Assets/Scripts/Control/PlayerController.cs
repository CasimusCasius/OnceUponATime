using Game.Combat;
using Game.Movement;
using UnityEngine;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {
            InteractWithCombat();

            InteractWithMovement();
        }

        private void InteractWithCombat()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                CombatTarget target = hit.transform.GetComponent<CombatTarget>();
                if (target == null)
                {
                    continue;
                }

                if (Input.GetMouseButtonDown(0))
                {
                    GetComponent<Fighter>()?.Attack(target);
                    break;
                }
            }
        }

        private void InteractWithMovement()
        {
            if (Input.GetMouseButton(0))
            {
                MoveToCursor();
            }
        }

        private void MoveToCursor()
        {


            if (Physics.Raycast(GetMouseRay(), out RaycastHit hitInfo))
            {
                GetComponent<Mover>()?.MoveTo(hitInfo.point);
            }
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
