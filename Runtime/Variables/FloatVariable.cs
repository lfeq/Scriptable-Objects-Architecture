using System;
using UnityEngine;
using UnityEngine.Events;

namespace Scriptable_Objects_Architecture.Runtime.Variables {
    [CreateAssetMenu(fileName = "Float Variable", menuName = "Scriptable Objects Architecture/Variables/Float Value")]
    public class FloatVariable : RuntimeScriptableObject {
        /// <summary>
        /// Event triggered when the value changes.
        /// </summary>
        public event UnityAction<float> OnValueChange = delegate { };

        /// <summary>
        /// Gets or sets the current value. Triggers the OnValueChange event if the value changes.
        /// </summary>
        public float Value {
            get => value;
            set {
                const float tolerance = 1e-6f;
                if(Math.Abs(this.value - value) < tolerance) return;
                this.value = value;
                OnValueChange.Invoke(this.value);
            }
        }
        
        [Tooltip("The initial value of the integer.")]
        [SerializeField] private float initialValue;
        
        [Tooltip("The current value of the integer.")]
        [SerializeField] private float value;
        
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