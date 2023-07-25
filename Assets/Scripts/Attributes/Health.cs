using Game.Core;
using Game.Saving;
using Game.Stats;
using UnityEngine;

namespace Game.Attribiutes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float healthPoints = 100f;
        private bool isAlive = true;

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetHealth();
        }

        public void TakeDamage(float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            print(healthPoints);
            if (healthPoints == 0 && isAlive)
            {
                Die();
            }
        }

        public float GetProcentage() 
        { 
        
            return healthPoints * 100 / GetComponent<BaseStats>().GetHealth();

        }



        private void Die()
        {
            isAlive = false;
            if (TryGetComponent<ActionScheduler>(out ActionScheduler actionScheduler))
            {
                actionScheduler.CancelCurrentAction();
            }
            GetComponent<Animator>().SetTrigger("die");
        }

        public bool IsAlive() => isAlive;

        public object CaptureState()
        {
            return healthPoints;
        }

        public void RestoreState(object state)
        {
            healthPoints = (float)state;

            if (healthPoints <= 0)
                Die();
        }
    }
}
