using Game.Combat;
using Game.Core;
using Game.Movement;
using UnityEngine;

namespace Game.Control
{
    public class AIController : MonoBehaviour
    {
        [SerializeField] private float chaseDistance = 5f;
        [SerializeField] private float timeOfSuspition = 3f;
        [SerializeField] private PatrolPath patrolPath;
        [SerializeField] private float waypointsTolerance = .5f;
        [SerializeField] private float dwellingTime = 1f;

        private Fighter fighter;
        private GameObject player;
        private Health myHealth;
        private Mover mover;

        Vector3 guardingPosition;
        float timeSinceLastSawPlayer = Mathf.Infinity;
        int currentWaypointIndex = 0;
        float timeSinceReachWaypoint = Mathf.Infinity;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            myHealth = GetComponent<Health>();
            mover = GetComponent<Mover>();
            guardingPosition = transform.position;
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (myHealth == null || !myHealth.IsAlive()) return;
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player))
            {
                AttackBehaviour();
            }
            else if (timeSinceLastSawPlayer < timeOfSuspition)
            {
                SuspicionBehaviour();
            }
            else
            {
                PatrolBehaviour();
            }
            UpdateTimers();
        }

        private void UpdateTimers()
        {
            timeSinceLastSawPlayer += Time.deltaTime;
            timeSinceReachWaypoint += Time.deltaTime;
        }

        private void AttackBehaviour()
        {
            timeSinceLastSawPlayer = 0;
            fighter.Attack(player);
        }

        private void SuspicionBehaviour()
        {
            GetComponent<ActionScheduler>().CancelCurrentAction();
        }

        private void PatrolBehaviour()
        {
            Vector3 nextPosition = guardingPosition;

            if (patrolPath != null)
            {
                if (WaypointReached())
                {
                    CycleWaypoint();
                    timeSinceReachWaypoint = 0;
                }
                nextPosition = GetCurrentWaypoint();
            }
            if (timeSinceReachWaypoint > dwellingTime)
            {
                mover.StartMoveAction(nextPosition);
            }
        }

        private bool WaypointReached()
        {
            Vector3 flatCurrentPosition = Vector3.ProjectOnPlane(transform.position, Vector3.up);
            Vector3 flatPositionToReach = Vector3.ProjectOnPlane(GetCurrentWaypoint(), Vector3.up);

            if (Vector3.Distance(flatCurrentPosition, flatPositionToReach) < waypointsTolerance) return true;

            return false;
        }

        private void CycleWaypoint()
        {
            currentWaypointIndex = patrolPath.GetNextIndex(currentWaypointIndex);
        }

        private Vector3 GetCurrentWaypoint()
        {
            return patrolPath.GetWaypoint(currentWaypointIndex).position;
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer <= chaseDistance;
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.blue;
            Gizmos.DrawWireSphere(transform.position, chaseDistance);
        }
    }
}
