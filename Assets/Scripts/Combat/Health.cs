using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{
    public class Health : MonoBehaviour
    {
        [SerializeField] private float healthPoints = 100f;
        private bool isAlive = true;


        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            print(healthPoints);
            if (healthPoints == 0 && isAlive)
            {
                Die();
            }
        }

        private void Die()
        {
            isAlive = false;

            GetComponent<Animator>().SetTrigger("die");
        }

        public bool IsAlive() => isAlive;
    }
}
