using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.Variables {
    public class BoolVariableTests {
        private BoolVariable boolVariable;
        private bool eventTriggered;
        private bool eventValue;

        [SetUp]
        public void SetUp() {
            boolVariable = ScriptableObject.CreateInstance<BoolVariable>();
            typeof(BoolVariable).GetField("initialValue",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(boolVariable, true);
            boolVariable.Value = false;
            boolVariable.OnValueChange += OnValueChanged;
            eventTriggered = false;
        }

        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(boolVariable);
        }

        private void OnValueChanged(bool newValue) {
            eventTriggered = true;
            eventValue = newValue;
        }

        [Test]
        public void Value_SetValue_TriggersOnValueChange() {
            boolVariable.Value = true;
            Assert.IsTrue(eventTriggered);
            Assert.IsTrue(eventValue);
        }

        [Test]
        public void OnReset_ResetsValueToInitialValue() {
            boolVariable.Value = false;
            boolVariable.OnReset();
            Assert.IsTrue(boolVariable.Value, "Expected the value to be reset to initialValue (true).");
        }
    }
}