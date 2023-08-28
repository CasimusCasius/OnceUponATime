using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Game.Stats
{
	public class LevelDisplay : MonoBehaviour
	{
		[SerializeField] TextMeshProUGUI levelText= null;
		BaseStats baseStats = null;
        private void Awake()
        {
            baseStats = GameObject.FindWithTag("Player").GetComponent<BaseStats>();
        }
        private void Update()
        {
            levelText.text = baseStats.GetLevel().ToString();
        }
    } 
}
