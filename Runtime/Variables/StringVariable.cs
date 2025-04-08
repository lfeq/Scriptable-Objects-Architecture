using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptable_Objects_Architecture.Runtime.Variables {
    [CreateAssetMenu(fileName = "String Variable", menuName = "Scriptable Objects Architecture/Variables/String Value")]
    public class StringVariable : RuntimeScriptableObject {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event UnityAction<string> OnValueChange = delegate { };

        /// <summary>
        /// Gets or sets the current value. Triggers the OnValueChange event if the value changes.
        /// </summary>
        public string Value {
            get => value;
            set {
                if (string.Equals(this.value, value)) return;
                this.value = value;
                OnValueChange.Invoke(this.value);
            }
        }

        [Tooltip("The initial value of the string.")]
        [SerializeField] private string initialValue;
        
        [Tooltip("The current value of the string.")]
        [SerializeField] private string value;
        
        /// <summary>
        /// Resets the value to the initial value and triggers the OnValueChange event.
        /// </summary>
        public override void OnReset() {
            OnValueChange.Invoke(value = initialValue);
        }

        private void OnValidate() {
            OnValueChange.Invoke(value);
        }
    }
}