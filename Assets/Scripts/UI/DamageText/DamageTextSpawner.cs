using TMPro;
using UnityEngine;

namespace Game.UI.DamageText
{
    public class DamageTextSpawner : MonoBehaviour
    {
        [SerializeField] private DamageText damageTextPrefab;
       
        private void Start()
        {
           
        }
        public void Spawn(float damageAmount)
        {
            DamageText damageText = Instantiate(damageTextPrefab, transform);
            damageText.SetDamageValue(damageAmount);
        }
    }
}
