using Game.Movement;
using UnityEngine;

namespace Game.Control
{
    public class PlayerController : MonoBehaviour
    {

        void Update()
        {
            if (Input.GetMouseButton(0))
            {
                ProceedActionInCursor();
            }
        }

        private void ProceedActionInCursor()
        {
            Ray Ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            if (Physics.Raycast(Ray, out RaycastHit hitInfo))
            {
                GetComponent<Mover>()?.MoveTo(hitInfo.point);
            }
        }
    }
}
