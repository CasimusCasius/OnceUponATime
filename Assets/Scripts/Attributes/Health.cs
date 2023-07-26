
using Game.Core;
using Game.Saving;
using Game.Stats;
using UnityEngine;

namespace Game.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] private float healthPoints = 100f;
        private bool isAlive = true;

        private void Start()
        {
            healthPoints = GetComponent<BaseStats>().GetStat(Stat.Health);
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            healthPoints = Mathf.Max(healthPoints - damage, 0);

            //print(healthPoints);
            if (healthPoints == 0 && isAlive)
            { 
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetProcentage() 
        { 
        
            return healthPoints * 100 / GetComponent<BaseStats>().GetStat(Stat.Health);

        }

        private void Die()
        {
            isAlive = false;
            if (TryGetComponent(out ActionScheduler actionScheduler))
            {
                actionScheduler.CancelCurrentAction();
            }
            
            GetComponent<Animator>().SetTrigger("die");
            GetComponent<Collider>().enabled = false;
        }

        private void AwardExperience(GameObject instigator)
        {
            if (instigator.TryGetComponent(out Experience experience))
            {
                experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.Experience));
            }
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
