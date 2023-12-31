﻿using Game.Attributes;
using Game.Core;
using Game.Movement;
using Game.Saving;
using Game.Stats;
using RPG.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Combat
{

    public class Fighter : MonoBehaviour, IAction, ISaveable, IModifierProvider
    {
        [SerializeField] private float timeBetweenAttacks = 1.5f;
        [SerializeField] private Transform rightHandTransform = null;
        [SerializeField] private Transform leftHandTransform = null;
        [SerializeField] private Weapon defaultWeapon = null;
        
        private Health target;
        private float timeSinceLastAttack = Mathf.Infinity;
        private LazyValue<Weapon> currentWeapon;

        private void Awake()
        {
            currentWeapon = new LazyValue<Weapon>(SetupDefaultWeapon);
        }

        private Weapon SetupDefaultWeapon()
        {
            AttachWeapon(defaultWeapon);
            return defaultWeapon;
        }

        private void Start()
        {
            currentWeapon.ForceInit();
        }

        private void Update()
        {
            timeSinceLastAttack += Time.deltaTime;

            if (target == null) return;

            if (!target.IsAlive()) return;

            if (TryGetComponent(out Mover mover))
            {
                if (!GetIsInRange())
                {

                    mover.MoveTo(target.transform.position);

                }
                else
                {
                    mover.Cancel();

                    AttackBehaviour();
                }
            }
        }

        public void Attack(GameObject combatTarget)
        {
            GetComponent<ActionScheduler>().StartAction(this);
            target = combatTarget.GetComponent<Health>();
        }

        public void Cancel()
        {

            StopAttack();
            target = null;
            GetComponent<Mover>().Cancel();
        }

        public void EquipWeapon(Weapon weapon)
        {
            currentWeapon.value = weapon;
            AttachWeapon(weapon);
        }

        private void AttachWeapon(Weapon weapon)
        {
            Animator animator = GetComponent<Animator>();
            weapon.Spawn(rightHandTransform, leftHandTransform, animator);
        }

        public bool CanAttack(GameObject combatTarget)
        {
            return (combatTarget != null && combatTarget.GetComponent<Health>().IsAlive());
        }
        public Weapon GetCurrentWeapon() => currentWeapon.value;

        // animation Events
        public void Hit()
        {
            if (target == null) return;

            float damage = GetComponent<BaseStats>().GetStat(Stat.Damage);
            if (currentWeapon.value.HasProjectile())
            {
                currentWeapon.value.LaunchProjectile(
                    currentWeapon.value.GetHand(rightHandTransform, leftHandTransform),
                    target,
                    gameObject,
                    damage);
            }
            else
            {
                
                target.TakeDamage(gameObject, damage);
            }
        }

        public void Shoot()
        {                     
            Hit();
        }

        public Health GetTarget() => target;

        private void StopAttack()
        {
            if (TryGetComponent(out Animator animator))
                animator.ResetTrigger("attack");
            animator.SetTrigger("outAttack");
        }

        public IEnumerable<float> GetAdditiveModifiers(Stat stat)
        {
            if (stat==Stat.Damage)
            {
                yield return currentWeapon.value.GetWeaponDamage();
            }
        }
        public IEnumerable<float> GetPercentageModifiers(Stat stat)
        {
            if (stat==Stat.Damage)
            {
                yield return currentWeapon.value.GetPercentageBonus();
            }
        }

        private void AttackBehaviour()
        {

            transform.LookAt(target.transform);

            if (target != null && !target.IsAlive()) Cancel();

            if (timeSinceLastAttack > timeBetweenAttacks)
            {
                TriggerAttack();
                timeSinceLastAttack = 0;
            }
        }

        private void TriggerAttack()
        {
            if (TryGetComponent(out Animator animator))
            {
                animator.ResetTrigger("outAttack");
                animator.SetTrigger("attack");
            }
        }

        private bool GetIsInRange()
        {
            return
                Vector3.Distance(target.transform.position, transform.position) <= 
                currentWeapon.value.GetWeaponRange();
        }

        public object CaptureState()
        {
            return currentWeapon.value.name;
        }

        public void RestoreState(object state)
        {
            string weaponName = (string)state;
            EquipWeapon(Resources.Load<Weapon>(weaponName));
        }
    }
}