using System.Collections.Generic;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Runtime.Variables {
    /// <summary>
    /// Abstract base class for runtime scriptable objects.
    /// Manages instances and provides a reset mechanism.
    /// </summary>
    public abstract class RuntimeScriptableObject : ScriptableObject {
        /// <summary>
        /// List of all instances of RuntimeScriptableObject.
        /// </summary>
        private static readonly List<RuntimeScriptableObject> Instances = new();

        private void OnEnable() => Instances.Add(this);
        
        private void OnDisable() => Instances.Remove(this);

        /// <summary>
        /// Abstract method to reset the instance.
        /// Must be implemented by derived classes.
        /// </summary>
        public abstract void OnReset();

        /// <summary>
        /// Resets all instances before the scene loads.
        /// </summary>
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void ResetAllInstances() {
            foreach (var instance in Instances) {
                instance.OnReset();
            }
        }
    }
}