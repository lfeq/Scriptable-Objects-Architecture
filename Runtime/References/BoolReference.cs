using System;
using Scriptable_Objects_Architecture.Runtime.Variables;

namespace Scriptable_Objects_Architecture.Runtime.References {
    [Serializable]
    public class BoolReference {
        public bool UseConstant = true;
        public bool ConstantValue;
        public BoolVariable Variable;

        public BoolReference() { }

        public BoolReference(bool value) {
            UseConstant = true;
            ConstantValue = value;
        }

        public bool Value {
            get => UseConstant ? ConstantValue : Variable.Value;
            set {
                if (UseConstant) {
                    ConstantValue = value;
                    return;
                }
                Variable.Value = value;
            }
        }

        public static implicit operator bool(BoolReference reference) {
            return reference.Value;
        }
    }
}