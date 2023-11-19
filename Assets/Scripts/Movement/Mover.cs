using Game.Attributes;
using Game.Core;
using Game.Saving;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Movement
{
    public class Mover : MonoBehaviour, IAction, ISaveable
    {
        NavMeshAgent navMeshAgent;
        [SerializeField] private float maxMovementSpeed = 6f;

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        void Update()
        {
            navMeshAgent.enabled = GetComponent<Health>().IsAlive();
            UpdateAnimator();
        }

        private void UpdateAnimator()
        {
            Vector3 velocity = navMeshAgent.velocity;
            Vector3 localVelocity = transform.InverseTransformDirection(velocity);
            GetComponent<Animator>().SetFloat("forwardSpeed", localVelocity.z);
        }

        public void MoveTo(Vector3 destination)
        {
            navMeshAgent.destination = destination;
            navMeshAgent.isStopped = false;
        }

        public void Cancel()
        {
            navMeshAgent.isStopped = true;
        }

        public void StartMoveAction(Vector3 destination)
        {         
            GetComponent<ActionScheduler>().StartAction(this);
           
            MoveTo(destination);
        }

        public void SetMovementSpeed(float movementFraction)
        {
            navMeshAgent.speed = movementFraction * maxMovementSpeed;
        }

        public object CaptureState()
        {
            return new SerializableVector(transform.position);
        }

        public void RestoreState(object state)
        {
            if (state is SerializableVector position)
            {
                if (TryGetComponent<NavMeshAgent>(out var navMesh))
                {
                    navMesh.Warp((Vector3)position.ToVector());
                }
                GetComponent<ActionScheduler>().CancelCurrentAction();
            }
        }
    }
}