using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptable_Objects_Architecture.Runtime.Variables {
    /// <summary>
    /// ScriptableObject that holds an integer value and triggers an event when the value changes.
    /// </summary>
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Scriptable Objects Architecture/Variables/Int Value")]
    public class IntVariable : RuntimeScriptableObject {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event UnityAction<int> OnValueChange = delegate { };

        /// <summary>
        /// Gets or sets the current value. Triggers the OnValueChange event if the value changes.
        /// </summary>
        public int Value {
            get => value;
            set {
                if(this.value == value) return;
                this.value = value;
                OnValueChange.Invoke(this.value);
            }
        }
        
        [Tooltip("The initial value of the integer.")]
        [SerializeField] private int initialValue;
        
        [Tooltip("The current value of the integer.")]
        [SerializeField] private int value;
        
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