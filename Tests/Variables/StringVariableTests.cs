using NUnit.Framework;
using Scriptable_Objects_Architecture.Runtime.Variables;
using UnityEngine;

namespace Scriptable_Objects_Architecture.Tests.Variables {
    public class StringVariableTests {
        private StringVariable stringVariable;
        private bool eventTriggered;
        private string eventValue;

        [SetUp]
        public void SetUp() {
            stringVariable = ScriptableObject.CreateInstance<StringVariable>();
            typeof(StringVariable).GetField("initialValue",
                    System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(stringVariable, "Initial");
            stringVariable.Value = "Initial";
            stringVariable.OnValueChange += OnValueChanged;
            eventTriggered = false;
        }

        [TearDown]
        public void TearDown() {
            Object.DestroyImmediate(stringVariable);
        }

        private void OnValueChanged(string newValue) {
            eventTriggered = true;
            eventValue = newValue;
        }

        [Test]
        public void Value_SetValue_TriggersOnValueChange() {
            stringVariable.Value = "Changed";
            Assert.IsTrue(eventTriggered, "Expected event triggered to be true");
            Assert.AreEqual("Changed", eventValue, "Expected event value to be 'Changed'");
        }

        [Test]
        public void OnReset_ResetsValueToInitialValue() {
            stringVariable.Value = "Changed";
            stringVariable.OnReset();
            Assert.AreEqual("Initial", stringVariable.Value,
                "Expected the value to be reset to initialValue ('Initial').");
        }
    }
}