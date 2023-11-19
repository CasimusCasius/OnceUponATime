using Game.Attributes;
using Game.Movement;
using System;
using UnityEngine;
using UnityEngine.AI;
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
        [SerializeField] private float navMeshSearchingDistance = 5f;
        [SerializeField] private float maxNavMeshPathLenght = 40f;

        private ECursorType cursorType;
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

            SetCursor(ECursorType.None);
        }

        private bool InteractWithUI()
        {
            SetCursor(ECursorType.UI);
            return EventSystem.current.IsPointerOverGameObject();
        }
        private bool InteractWithComponent()
        {
            RaycastHit[] hits = RaycastsAllSorted();
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

        RaycastHit[] RaycastsAllSorted()
        {

            RaycastHit[] hits = Physics.RaycastAll(GetMouseRay());

            // Sort by distance
            float[] distances = new float[hits.Length];
            for (int i = 0; i < hits.Length; i++)
            {
                distances[i] = hits[i].distance;
            }
            Array.Sort(distances, hits); // sortuj po distances tablice hits

            return hits;
        }

        private bool InteractWithMovement()
        {
            if (RaycastNavMesh(out Vector3 target))
            {
                if (Input.GetMouseButton(0))
                {
                    GetComponent<Mover>().StartMoveAction(target);
                }
                SetCursor(ECursorType.Movement);
                return true;
            }
            return false;
        }

        private bool RaycastNavMesh(out Vector3 target)
        {
            target = new Vector3();
            if (!Physics.Raycast(GetMouseRay(), out RaycastHit hit)) return false;
            if (!NavMesh.SamplePosition(hit.point, out NavMeshHit closeHit,
                navMeshSearchingDistance, NavMesh.AllAreas)) return false;

            target = closeHit.position;
            NavMeshPath path = new NavMeshPath();

            if (!NavMesh.CalculatePath(transform.position, target, NavMesh.AllAreas, path))
                return false;
            if (path.status != NavMeshPathStatus.PathComplete)
                return false;
            if (GetPathLenght(path) > maxNavMeshPathLenght) return false;

            return true;
        }

        private float GetPathLenght(NavMeshPath path)
        {
            float distance = 0;
            if (path.corners.Length < 2) return distance;
            for (int i = 1; i < path.corners.Length; i++)
            {
                distance += Vector3.Distance(path.corners[i - 1], path.corners[i]);
            }
            return distance;
        }

        private void SetCursor(ECursorType type)
        {
            if (type == cursorType) return;

            CursorMapping mapping = GetCursorMapping(type);
            Cursor.SetCursor(mapping.texture, mapping.hotspot, CursorMode.ForceSoftware);
            cursorType = type;

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
