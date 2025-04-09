using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.Variables {
    public class FloatVariableTests {
        private FloatVariable floatVariable;
        private bool eventTriggered;
        private float eventValue;
        
        [SetUp]
        public void SetUp() {
            floatVariable = ScriptableObject.CreateInstance<FloatVariable>();
            typeof(FloatVariable).GetField("initialValue",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(floatVariable, 1.0f);
            floatVariable.Value = 0.0f;
            floatVariable.OnValueChange += OnValueChanged;
            eventTriggered = false;
        }
        
        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(floatVariable);
        }
        
        private void OnValueChanged(float newValue) {
            eventTriggered = true;
            eventValue = newValue;
        }
        
        [Test]
        public void Value_SetValue_TriggersOnValueChange() {
            floatVariable.Value = 2.0f;
            Assert.IsTrue(eventTriggered, "Expected event triggered to be true");
            Assert.AreEqual(2.0f, eventValue, "Expected event value to be 2.0f");
        }
        
        [Test]
        public void OnReset_ResetsValueToInitialValue() {
            floatVariable.Value = 0.0f;
            floatVariable.OnReset();
            Assert.AreEqual(1.0f, floatVariable.Value, "Expected the value to be reset to initialValue (1.0f).");
        }
    }
}