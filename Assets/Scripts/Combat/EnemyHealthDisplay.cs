using Game.Attributes;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Combat
{
    public class EnemyHealthDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI healthValue = null;
        Fighter fighter;

        private void Awake()
        {
            fighter = GameObject.FindWithTag("Player").GetComponent<Fighter>();
        }


        private void Update()
        {

            if (fighter.GetTarget() != null)
                healthValue.text = String.Format("{0:0} / {1:0}",
                    fighter.GetTarget().GetHealthPoints(),
                    fighter.GetTarget().GetMaxHealthPoints());
            else
                healthValue.text = "N/A";

        }


    }
}
