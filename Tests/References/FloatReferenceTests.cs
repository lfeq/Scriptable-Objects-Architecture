using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.References;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.References {
    public class FloatReferenceTests {
        [Test]
        public void Value_ReturnsConstant_WhenUseConstantIsTrue() {
            var floatReference = new FloatReference(1.0f) {
                UseConstant = true,
                ConstantValue = 5.0f
            };
            
            Assert.AreEqual(5.0f, floatReference.Value, "Expected Value to return ConstantValue when UseConstant is true.");
        }
        
        [Test]
        public void Value_ReturnsVariableValue_WhenUseConstantIsFalse() {
            var floatReference = new FloatReference(1.0f) {
                UseConstant = false,
                ConstantValue = 5.0f
            };
            var floatVariable = ScriptableObject.CreateInstance<FloatVariable>();
            floatVariable.Value = 10.0f;
            floatReference.Variable = floatVariable;
            
            Assert.AreEqual(10.0f, floatReference.Value, "Expected Value to return Variable.Value when UseConstant is false.");
        }
    }
}