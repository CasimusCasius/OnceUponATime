using UnityEngine;

namespace Game.Control
{
    public class PatrolPath : MonoBehaviour
    {
        [SerializeField] float radiusOfWaypointSphere = 0.25f;
        private void OnDrawGizmos()
        {
            for (int i = 0; i < transform.childCount; i++)
            {
                Gizmos.DrawSphere(GetWaypoint(i).position, radiusOfWaypointSphere);
                Gizmos.DrawLine(GetWaypoint(i).position, GetWaypoint(GetNextIndex(i)).position);
            }
        }

        public Transform GetWaypoint(int i)
        {
            return transform.GetChild(i);
        }
        public int GetNextIndex(int i)
        {
            return ((i + 1) % transform.childCount);
        }
    }
}
