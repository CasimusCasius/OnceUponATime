using Game.Attributes;
using Game.Control;
using UnityEngine;

namespace Game.Combat
{
    [RequireComponent(typeof(Health))]
    public class CombatTarget : MonoBehaviour, IRaycastable
    {
        public bool HandleRaycast(PlayerController callingController)
        {
            Fighter fighter = callingController.GetComponent<Fighter>();
            if (Input.GetMouseButton(0) && fighter.CanAttack(this.gameObject))
            {
                fighter.Attack(this.gameObject);
                
            }
            return true;
        }

        public ECursorType GetCursorType() => ECursorType.Combat;
    }
}