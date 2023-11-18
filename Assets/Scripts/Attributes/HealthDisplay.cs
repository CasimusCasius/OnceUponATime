using System;
using TMPro;
using UnityEngine;

namespace Game.Attributes
{
    public class HealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthValue = null;
        Health health;

        private void Awake()
        {
            health = GameObject.FindWithTag("Player").GetComponent<Health>();
        }

        private void Update()
        {
            healthValue.text = String.Format("{0:0} / {1:0}",
                health.GetHealthPoints(), health.GetMaxHealthPoints());
        }


    }
}
