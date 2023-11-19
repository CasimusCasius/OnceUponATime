using Game.Attributes;
using Game.Movement;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Game.Control
{

    public class PlayerController : MonoBehaviour
    {
        [System.Serializable]
        struct CursorMapping
        {
            public ECursorType type;
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
            if (InteractWithUI()) return;
            if (myHealth == null || !myHealth.IsAlive())
            {
                SetCursor(ECursorType.None);
                return;
            }
            if (InteractWithComponent()) return;
            if (InteractWithMovement()) return;
            //print("Nothing happend");
            SetCursor(ECursorType.None);
        }

        private bool InteractWithUI()
        {
            SetCursor(ECursorType.UI);
            return EventSystem.current.IsPointerOverGameObject();
        }
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());
            foreach (RaycastHit hit in hits)
            {
                IRaycastable[] raycastables = hit.transform.GetComponents<IRaycastable>();
                foreach (var raycastable in raycastables)
                {
                    if (raycastable.HandleRaycast(this))
                    {
                        SetCursor(raycastable.GetCursorType());
                        return true;
                    }
                }
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
                SetCursor(ECursorType.Movement);
                return true;
            }
            return false;
        }

        private void SetCursor(ECursorType type)
        {
            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.Auto);
        }

        private CursorMapping GetCursorMapping(ECursorType type)
        {
            foreach (CursorMapping mapping in cursorMappings)
            {
                if (mapping.type == type)
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
