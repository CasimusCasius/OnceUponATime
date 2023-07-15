using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Game.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdenntifier = "";
        static Dictionary<string, SaveableEntity> globalLookup = new Dictionary<string, SaveableEntity>();

#if UNITY_EDITOR
        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;

            if (string.IsNullOrWhiteSpace(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty(nameof(uniqueIdenntifier));

            if (string.IsNullOrWhiteSpace(property.stringValue) || !IsUnique(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
            globalLookup[property.stringValue] = this;
        }

        private bool IsUnique(string candidate)
        {
            if (!globalLookup.ContainsKey(candidate)) return true;
            if (globalLookup[candidate] == this) return true;
            if (globalLookup[candidate] == null)
            {
                globalLookup.Remove(candidate);
                return true;
            }
            if (globalLookup[candidate].GetUniqueIdentifier() != candidate)
            {
                globalLookup.Remove(candidate);
                return true;
            }

            return false;
        }
#endif

        public string GetUniqueIdentifier()
        {
            return uniqueIdenntifier;
        }

        public object CaptureState()
        {
            Dictionary<string, object> state = new Dictionary<string, object>();
            foreach (var saveable in GetComponents<ISaveable>())
            {
                state[saveable.GetType().ToString()] = saveable.CaptureState();
            }
            return state;

        }

        public void RestoreState(object state)
        {
            Dictionary<string, object> restoreState = state as Dictionary<string, object>;
            if (restoreState == null) return;
            foreach (var saveable in GetComponents<ISaveable>())
            {
                string typeString = saveable.GetType().ToString();
                if (restoreState.ContainsKey(typeString))
                    saveable.RestoreState(restoreState[typeString]);
            }


        }
    }
}