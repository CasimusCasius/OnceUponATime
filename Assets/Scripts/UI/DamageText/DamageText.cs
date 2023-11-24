using System;
using TMPro;
using UnityEngine;

namespace Game.UI.DamageText
{
    public class DamageText : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI textField;
        public void DestroyText()
        {
            Destroy(gameObject);
        }

        public void SetDamageValue(float damageValue)
        {
            textField.text = String.Format("{0:0}",damageValue);
        }


    }
}
