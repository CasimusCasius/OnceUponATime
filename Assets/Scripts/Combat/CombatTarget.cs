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
            throw new System.NotImplementedException();
        }
    }
}