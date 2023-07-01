
using Game.Core;
using System;
using UnityEngine;
using UnityEngine.AI;

namespace Game.Movement
{
   
    public class Mover : MonoBehaviour, IAction
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
            Debug.Log(this);
        }

        public void StartMoveAction(Vector3 destination)
        {
                         
            GetComponent<ActionScheduler>().StartAction(this);
           
            MoveTo(destination);
        }

        internal void SetMovementSpeed(float movementFraction)
        {
            navMeshAgent.speed = movementFraction * maxMovementSpeed;
        }
    }
}