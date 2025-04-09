using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.References;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.References {
    public class BoolReferenceTests {
        [Test]
        public void Value_ReturnsConstant_WhenUseConstantIsTrue() {
            var boolReference = new BoolReference(true) {
                UseConstant = true,
                ConstantValue = false
            };
            
            Assert.IsFalse(boolReference.Value, "Expected Value to return ConstantValue when UseConstant is true.");
        }

        [Test]
        public void Value_ReturnsVariableValue_WhenUseConstantIsFalse() {
            var boolReference = new BoolReference(true) {
                UseConstant = false,
                ConstantValue = false
            };
            var boolVariable = ScriptableObject.CreateInstance<BoolVariable>();
            boolVariable.Value = true;
            boolReference.Variable = boolVariable;
            
            Assert.IsTrue(boolReference.Value, "Expected Value to return Variable.Value when UseConstant is false.");
        }
    }
}