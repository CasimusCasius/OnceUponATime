using Game.Combat;
using Game.Core;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Movement
{
    public class Mover : MonoBehaviour
    {
        NavMeshAgent navMeshAgent;
        

        private void Awake()
        {
            navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        void Update()
        {
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

        public void Stop()
        {
            navMeshAgent.isStopped = true;
        }

        public void StartMoveAction(Vector3 destination)
        {
            GetComponent<ActionScheduler>()?.StartAction(this);
            GetComponent<Fighter>()?.Cancel();
            MoveTo(destination);
        }
    }
}