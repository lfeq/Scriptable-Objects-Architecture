using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.References;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.References {
    public class StringReferenceTests {
        [Test]
        public void Value_ReturnsConstant_WhenUseConstantIsTrue() {
            var stringReference = new StringReference {
                UseConstant = true,
                ConstantValue = "Hello"
            };
            
            Assert.AreEqual("Hello", stringReference.Value, "Expected Value to return ConstantValue when UseConstant is true.");
        }
        
        [Test]
        public void Value_ReturnsVariableValue_WhenUseConstantIsFalse() {
            var stringReference = new StringReference {
                UseConstant = false,
                ConstantValue = "Hello"
            };
            var stringVariable = ScriptableObject.CreateInstance<StringVariable>();
            stringVariable.Value = "World";
            stringReference.Variable = stringVariable;
            
            Assert.AreEqual("World", stringReference.Value, "Expected Value to return Variable.Value when UseConstant is false.");
        }
    }
}