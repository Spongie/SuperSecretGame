using Assets.Scripts.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Attacks
{
    [CustomPropertyDrawer(typeof(AttackEffect))]
    public class AttackEffectDrawer : PropertyDrawer
    {
            // Draw the property inside the given rect
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                var attackEffects = new AttackEffectLoader().GetAttackMethods();
                //label.text = "Attack Effect";
                // Using BeginProperty / EndProperty on the parent property means that
                // prefab override logic works on the entire property.
                EditorGUI.BeginProperty(position, label, property);

                // Draw label
                position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

                // Don't make child fields be indented
                var indent = EditorGUI.indentLevel;
                EditorGUI.indentLevel = 0;

                // Calculate rects
                //var nameRect = new Rect(position.x, position.y, 60, position.height);
                var popupRect = EditorGUI.PrefixLabel(position,
                                              GUIUtility.GetControlID(FocusType.Passive),
                                              GUIContent.none);
                popupRect.width = 60;

                int currentIndex = attackEffects.IndexOf(property.FindPropertyRelative("Name").name);

                int newClassIndex = EditorGUI.Popup(popupRect, currentIndex, attackEffects.ToArray());

                if (newClassIndex != -1)
                    property.FindPropertyRelative("Name").stringValue = attackEffects[newClassIndex];

                var unitRect = new Rect(position.x + 65, position.y, 40, position.height);

                //EditorGUI.
                //// Draw fields - passs GUIContent.none to each so they are drawn without labels
                //EditorGUI.PropertyField(nameRect, property.FindPropertyRelative("Name"), GUIContent.none);
                EditorGUI.PropertyField(unitRect, property.FindPropertyRelative("Power"), GUIContent.none);

                // Set indent back to what it was
                EditorGUI.indentLevel = indent;

                EditorGUI.EndProperty();
            }
    }
}
