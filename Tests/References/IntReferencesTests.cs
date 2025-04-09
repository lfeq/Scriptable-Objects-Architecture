using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.References;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.References {
    public class IntReferencesTests {
        [Test]
        public void Value_ReturnsConstant_WhenUseConstantIsTrue() {
            var intReference = new IntReference {
                UseConstant = true,
                ConstantValue = 5
            };
            
            Assert.AreEqual(5, intReference.Value, "Expected Value to return ConstantValue when UseConstant is true.");
        }
        
        [Test]
        public void Value_ReturnsVariableValue_WhenUseConstantIsFalse() {
            var intReference = new IntReference {
                UseConstant = false,
                ConstantValue = 5
            };
            var intVariable = ScriptableObject.CreateInstance<IntVariable>();
            intVariable.Value = 10;
            intReference.Variable = intVariable;
            
            Assert.AreEqual(10, intReference.Value, "Expected Value to return Variable.Value when UseConstant is false.");
        }
    }
}