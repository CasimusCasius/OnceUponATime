using System.Collections;
using UnityEngine;
using UnityEditor;

namespace Game.Saving
{
    [ExecuteAlways]
    public class SaveableEntity : MonoBehaviour
    {
        [SerializeField] private string uniqueIdenntifier = "";

        private void Update()
        {
            if (Application.IsPlaying(gameObject)) return;

            if (string.IsNullOrWhiteSpace(gameObject.scene.path)) return;

            SerializedObject serializedObject = new SerializedObject(this);
            SerializedProperty property = serializedObject.FindProperty(nameof(uniqueIdenntifier));

            if (string.IsNullOrWhiteSpace(property.stringValue))
            {
                property.stringValue = System.Guid.NewGuid().ToString();
                serializedObject.ApplyModifiedProperties();
            }
        }

        public string GetUniqueIdentifier()
        {
            return uniqueIdenntifier;
        }

        public object CaptureState()
        {
            Debug.Log("Capturing state for " + GetUniqueIdentifier());
            return null;
        }

        public void RestoreState(object state)
        {
            Debug.Log("Restoring state for: " + GetUniqueIdentifier());
        }
    }
}