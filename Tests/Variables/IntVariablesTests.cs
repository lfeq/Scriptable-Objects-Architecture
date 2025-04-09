using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.Variables {
    public class IntVariablesTests {
        private IntVariable intVariable;
        private bool eventTriggered;
        private int eventValue;
        
        [SetUp]
        public void SetUp() {
            intVariable = ScriptableObject.CreateInstance<IntVariable>();
            typeof(IntVariable).GetField("initialValue",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(intVariable, 10);
            intVariable.Value = 0;
            intVariable.OnValueChange += OnValueChanged;
            eventTriggered = false;
        }
        
        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(intVariable);
        }
        
        private void OnValueChanged(int newValue) {
            eventTriggered = true;
            eventValue = newValue;
        }
        
        [Test]
        public void Value_SetValue_TriggersOnValueChange() {
            intVariable.Value = 5;
            Assert.IsTrue(eventTriggered, "Expected event triggered to be true");
            Assert.AreEqual(5, eventValue, "Expected event value to be 5");
        }

        [Test]
        public void OnReset_ResetsValueToInitialValue() {
            intVariable.Value = 0;
            intVariable.OnReset();
            Assert.AreEqual(10, intVariable.Value, "Expected the value to be reset to initialValue (10).");
        }
    }
}