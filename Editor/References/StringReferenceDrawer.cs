using System;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;
using System.Collections.Generic;
using Scriptable_Objects_Architecture.Runtime.References;
using Scriptable_Objects_Architecture.Runtime.Variables;

namespace Scriptable_Objects_Architecture.Editor.References {
    [CustomPropertyDrawer(typeof(StringReference))]
    public class StringReferenceDrawer : PropertyDrawer {
        public override VisualElement CreatePropertyGUI(SerializedProperty property) {
            var mainContainer = new VisualElement();
            mainContainer.style.flexDirection = FlexDirection.Row;

            var label = new Label(property.displayName);
            label.style.width = EditorGUIUtility.labelWidth;
            label.style.unityTextAlign = TextAnchor.MiddleLeft;

            var controlContainer = new VisualElement();
            controlContainer.style.flexDirection = FlexDirection.Row;
            controlContainer.style.flexGrow = 1;

            SerializedProperty useConstantProp = property.FindPropertyRelative("UseConstant");
            SerializedProperty constantValueProp = property.FindPropertyRelative("ConstantValue");
            SerializedProperty variableProp = property.FindPropertyRelative("Variable");

            var options = new List<string> { "Use Constant", "Use Variable" };
            var popup = new PopupField<string>(options, useConstantProp.boolValue ? 0 : 1);
            popup.style.width = 100;

            var textField = new TextField();
            textField.BindProperty(constantValueProp);
            textField.style.flexGrow = 1;
            var objectField = new ObjectField();
            objectField.objectType = typeof(StringVariable);
            objectField.BindProperty(variableProp);
            objectField.style.flexGrow = 1;

            var fieldContainer = new VisualElement();
            fieldContainer.style.flexGrow = 1;

            Action<bool> updateFieldDisplay = (useConstant) => {
                fieldContainer.Clear();
                fieldContainer.Add(useConstant ? textField : objectField);
            };

            updateFieldDisplay(useConstantProp.boolValue);

            popup.RegisterValueChangedCallback(evt => {
                bool useConstant = evt.newValue == options[0];
                useConstantProp.boolValue = useConstant;
                useConstantProp.serializedObject.ApplyModifiedProperties();
                updateFieldDisplay(useConstant);
            });

            controlContainer.Add(popup);
            controlContainer.Add(fieldContainer);
            mainContainer.Add(label);
            mainContainer.Add(controlContainer);
            return mainContainer;
        }
    }
}
