
using Game.Core;
using Game.Saving;
using Game.Stats;
using RPG.Utils;
using UnityEngine;

namespace Game.Attributes
{
    public class Health : MonoBehaviour, ISaveable
    {
        [SerializeField] float regenerationPercentage = 30f;
        private LazyValue<float> healthPoints;
        private bool isAlive = true;

        private void Awake()
        {
            healthPoints = new LazyValue<float> (GetMaxHealthPoints);
        }


        private void Start()
        {
            healthPoints.ForceInit ();
        }
        private void OnEnable()
        {
            GetComponent<BaseStats>().onLevelUp += Health_onLevelUp;
        }
        private void OnDisable()
        {
            GetComponent<BaseStats>().onLevelUp -= Health_onLevelUp;
        }

        private void Health_onLevelUp()
        {
            float regenHP = GetMaxHealthPoints() * (regenerationPercentage / 100);
            healthPoints.value = Mathf.Min(healthPoints.value + regenHP,
                GetMaxHealthPoints());
        }

        public void TakeDamage(GameObject instigator, float damage)
        {
            //Debug.Log(gameObject.name + " took damage: " +  damage);

            healthPoints.value = Mathf.Max(healthPoints.value - damage, 0);

            //print(healthPoints);
            if (healthPoints.value == 0 && isAlive)
            {
                Die();
                AwardExperience(instigator);
            }
        }

        public float GetProcentage() => 
            healthPoints.value * 100 / GetMaxHealthPoints();

        public float GetHealthPoints() => healthPoints.value;

        public float GetMaxHealthPoints() => GetComponent<BaseStats>().GetStat(EStat.Health);


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
                experience.GainExperience(GetComponent<BaseStats>().GetStat(EStat.ExperienceReward));
            }
        }

        public bool IsAlive() => isAlive;

        public object CaptureState()
        {
            return healthPoints.value;
        }

        public void RestoreState(object state)
        {
            healthPoints.value = (float)state;

            if (healthPoints.value <= 0)
                Die();
        }
    }
}
