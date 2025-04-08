using System;
using Scriptable_Objects_Architecture.Runtime.Variables;

namespace Scriptable_Objects_Architecture.Runtime.References {
    [Serializable]
    public class IntReference {
        public bool UseConstant = true;
        public int ConstantValue;
        public IntVariable Variable;

        public IntReference() { }

        public IntReference(int value) {
            UseConstant = true;
            ConstantValue = value;
        }

        public int Value {
            get => UseConstant ? ConstantValue : Variable.Value;
            set {
                if (UseConstant) {
                    ConstantValue = value;
                    return;
                }
                Variable.Value = value;
            }
        }

        public static implicit operator int(IntReference reference) {
            return reference.Value;
        }
    }
}