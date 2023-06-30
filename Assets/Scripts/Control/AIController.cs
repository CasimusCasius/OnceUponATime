using Game.Combat;
using Game.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Control
{
	public class AIController : MonoBehaviour
	{
        [SerializeField] private float chaseDistance = 5f;

        Fighter fighter;
        GameObject player;
        Health myHealth;

        private void Awake()
        {
            fighter = GetComponent<Fighter>();
            myHealth = GetComponent<Health>();
        }

        private void Start()
        {
            player = GameObject.FindWithTag("Player");
        }

        private void Update()
        {
            if (myHealth == null || !myHealth.IsAlive()) return;
            if (InAttackRangeOfPlayer() && fighter.CanAttack(player) )
            {
                fighter.Attack(player);
            }
            else
            {
                fighter.Cancel();
            }
        }

        private bool InAttackRangeOfPlayer()
        {
            float distanceToPlayer = Vector3.Distance(player.transform.position, transform.position);
            return distanceToPlayer <= chaseDistance;
        }
    } 
}
