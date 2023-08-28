
using Game.Core;
using Game.Saving;
using Game.Stats;
using UnityEngine;

namespace Game.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 30f;
        private float healthPoints = -1f;
        private bool isAlive = true;

        private void Start()
        {
            GetComponent<BaseStats>().onLevelUp += Health_onLevelUp;
            if (healthPoints < 0f)
                healthPoints = GetMaxHealthPoints();

        }

        private void Health_onLevelUp()
        {
            float regenHP = GetMaxHealthPoints() * (regenerationPercentage / 100);
            healthPoints = Mathf.Min(healthPoints + regenHP,
                GetMaxHealthPoints());
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            Debug.Log(gameObject.name + " took damage: " +  damage);

            healthPoints = Mathf.Max(healthPoints - damage, 0);

            //print(healthPoints);
            if (healthPoints == 0 && isAlive)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetProcentage() => 
            healthPoints * 100 / GetMaxHealthPoints();

        public float GetHealthPoints() => healthPoints;

        public float GetMaxHealthPoints() => GetComponent<BaseStats>().GetStat(Stat.Health);


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
                experience.GainExperience(GetComponent<BaseStats>().GetStat(Stat.ExperienceReward));
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
