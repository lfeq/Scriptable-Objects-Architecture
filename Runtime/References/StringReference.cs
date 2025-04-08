using System;
using Scriptable_Objects_Architecture.Runtime.Variables;

namespace Scriptable_Objects_Architecture.Runtime.References {
    [Serializable]
    public class StringReference {
        public bool UseConstant = true;
        public string ConstantValue;
        public StringVariable Variable;
        
        public StringReference() { }

        public StringReference(string value) {
            UseConstant = true;
            ConstantValue = value;
        }
        
        public string Value {
            get => UseConstant ? ConstantValue : Variable.Value;
            set {
                if (UseConstant) {
                    ConstantValue = value;
                    return;
                }
                Variable.Value = value;
            }
        }
        
        public static implicit operator string(StringReference reference) {
            return reference.Value;
        }
    }
}