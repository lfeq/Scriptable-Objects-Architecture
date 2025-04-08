using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using Scriptable_Objects_Architecture.Runtime.References;
using Scriptable_Objects_Architecture.Runtime.Variables;

namespace Scriptable_Objects_Architecture.Editor.References {
    [CustomPropertyDrawer(typeof(IntReference))]
    public class IntReferenceDrawer : PropertyDrawer {
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var mainContainer = new VisualElement();
            mainContainer.style.flexDirection = FlexDirection.Row;

            // Create property label
            var label = new Label(property.displayName);
            label.style.width = EditorGUIUtility.labelWidth;
            label.style.unityTextAlign = TextAnchor.MiddleLeft;

            // Control container for popup and value field
            var controlContainer = new VisualElement();
            controlContainer.style.flexDirection = FlexDirection.Row;
            controlContainer.style.flexGrow = 1;

            // Get serialized properties
            SerializedProperty useConstantProp = property.FindPropertyRelative("UseConstant");
            SerializedProperty constantValueProp = property.FindPropertyRelative("ConstantValue");
            SerializedProperty variableProp = property.FindPropertyRelative("Variable");

            // Create popup field
            var options = new List<string> { "Use Constant", "Use Variable" };
            var popup = new PopupField<string>(options, useConstantProp.boolValue ? 0 : 1);
            popup.style.width = 100;

            // Create fields: integer field and object field
            var intField = new IntegerField();
            intField.BindProperty(constantValueProp);
            intField.style.flexGrow = 1;
            var objectField = new ObjectField();
            objectField.objectType = typeof(IntVariable);
            objectField.BindProperty(variableProp);
            objectField.style.flexGrow = 1;

            // Container for dynamic fields
            var fieldContainer = new VisualElement();
            fieldContainer.style.flexGrow = 1;

            // Update field display based on UseConstant
            Action<bool> updateFieldDisplay = (useConstant) => {
                fieldContainer.Clear();
                fieldContainer.Add(useConstant ? intField : objectField);
            };

            // Initial setup
            updateFieldDisplay(useConstantProp.boolValue);

            // Popup change handler
            popup.RegisterValueChangedCallback(evt => {
                bool useConstant = evt.newValue == options[0];
                useConstantProp.boolValue = useConstant;
                useConstantProp.serializedObject.ApplyModifiedProperties();
                updateFieldDisplay(useConstant);
            });

            // Assemble UI
            controlContainer.Add(popup);
            controlContainer.Add(fieldContainer);
            mainContainer.Add(label);
            mainContainer.Add(controlContainer);
            return mainContainer;
        }
    }
}
