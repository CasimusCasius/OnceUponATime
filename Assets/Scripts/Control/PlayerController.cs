using Game.Attributes;
using Game.Combat;
using Game.Movement;
using System;
using UnityEngine;

namespace Game.Control
{

    public class PlayerController : MonoBehaviour
    {
        enum CursorType
        {
            None,
            Movement,
            Combat
        }

        [System.Serializable]
        struct CursorMapping
        {
            public CursorType type;
            public Texture2D texture;
            public Vector2 hotspot;
        }

        Health myHealth;
        [SerializeField] private CursorMapping[] cursorMappings = null;

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
            SetCursor(CursorType.None);
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
                SetCursor(CursorType.Combat);
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
                SetCursor(CursorType.Movement);
                return true;
            }
            return false;
        }

        private void SetCursor(CursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(CursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if( mapping.type == type)
                {
                    return mapping;
                }
            }
            return cursorMappings[0];
        }

        private static Ray GetMouseRay()
        {
            return Camera.main.ScreenPointToRay(Input.mousePosition);
        }
    }
}
