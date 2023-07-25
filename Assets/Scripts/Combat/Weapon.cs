using Game.Attribiutes;
using UnityEngine;

namespace Game.Combat
{
    [CreateAssetMenu(fileName = "Weapon", menuName = "Weapons/Make New Weapon", order = 0)]
    public class Weapon : ScriptableObject
    {
        [SerializeField] private AnimatorOverrideController animatorOverride = null;
        [SerializeField] private GameObject equippedPrefab = null;
        [SerializeField] private float weaponRange = 3f;
        [SerializeField] private float weaponDamage = 20f;
        [SerializeField] private bool isRightHanded = true;
        [SerializeField] private Projectile projectile = null;

        const string WEAPON_NAME = "Weapon";

        public void Spawn(Transform rightHand, Transform leftHand, Animator animator)
        {
            DestroyOldWeapon(rightHand, leftHand);

            if (equippedPrefab != null)
            {
                Transform handTransform = GetHand(rightHand, leftHand);

                GameObject weapon = Instantiate(equippedPrefab, handTransform);
                weapon.name = WEAPON_NAME;
            }

            var overrideController = animator.runtimeAnimatorController as AnimatorOverrideController;
            if (animatorOverride != null)
            {
                animator.runtimeAnimatorController = animatorOverride;
            }
            else if (overrideController != null)
            {
                animator.runtimeAnimatorController = overrideController.runtimeAnimatorController;
            }
        }

        public Transform GetHand(Transform rightHand, Transform leftHand)
        {
            return isRightHanded ? rightHand : leftHand;
        }

        public void LaunchProjectile(Transform hand, Health target)
        {
            var projectileInstance = Instantiate(projectile, hand.position, Quaternion.identity);
            projectileInstance.SetTarget(target, GetWeaponDamage());
        }

        public bool HasProjectile() => projectile != null;
        public float GetWeaponRange() => weaponRange;
        public float GetWeaponDamage() => weaponDamage;

        private void DestroyOldWeapon(Transform rightHand, Transform leftHand)
        {
            Transform oldWeapon = rightHand.Find(WEAPON_NAME);
            if (oldWeapon == null)
            {
                oldWeapon = leftHand.Find(WEAPON_NAME);
            }
            if (oldWeapon == null) return;

            oldWeapon.name = " DESTROYING";
            Destroy(oldWeapon.gameObject);
        }
    }
}