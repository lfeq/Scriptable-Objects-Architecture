using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptable_Objects_Architecture.Runtime.Variables {
    [CreateAssetMenu(fileName = "Bool Variable", menuName = "Scriptable Objects Architecture/Variables/Bool Value")]
    public class BoolVariable : RuntimeScriptableObject {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event UnityAction<bool> OnValueChange = delegate { };

        /// <summary>
        /// Gets or sets the current value. Triggers the OnValueChange event if the value changes.
        /// </summary>
        public bool Value {
            get => value;
            set {
                if (this.value == value) return;
                this.value = value;
                OnValueChange.Invoke(this.value);
            }
        }

        [Tooltip("The initial value of the bool.")]
        [SerializeField] private bool initialValue;
        
        [Tooltip("The current value of the bool.")]
        [SerializeField] private bool value;
        
        /// <summary>
        /// Resets the value to the initial value and triggers the OnValueChange event.
        /// </summary>
        public override void OnReset() {
            OnValueChange.Invoke(value = initialValue);
        }
        
        protected void OnValidate() {
            OnValueChange.Invoke(value);
        }
    }
}