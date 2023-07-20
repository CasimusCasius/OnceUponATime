using UnityEditor;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(fileName ="Weapon",menuName ="Weapons/Make New Weapon",order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private GameObject equippedPrefab = null;
        [SerializeField] private float weaponRange = 3f;
        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private bool isRightHanded = true;

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {

            if (equippedPrefab != null)
            {
                Transform handTransform = isRightHanded ? rightHand : leftHand;

                Instantiate(equippedPrefab, handTransform);
            }
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
        }

        public float GetWeaponRange() => weaponRange;
        public float GetWeaponDamage() => weaponDamage;
    }
}