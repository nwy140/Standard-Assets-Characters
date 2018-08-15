﻿using Attributes;
using UnityEditor;
using UnityEditor.VersionControl;
using UnityEngine;

namespace Editor
{
#if UNITY_EDITOR
    [CustomPropertyDrawer(typeof(HelperBoxAttribute))]
    public class HelperBoxPropertyDrawer : PropertyDrawer
    {
        private const float k_Spacing = 3f;
        private const float k_IconSize = 50f;
        private float height = 0;
        private float textHeight = 0;
        
        private HelperBoxAttribute helpAttribute
        {
            get { return (HelperBoxAttribute) attribute; }
        }

        public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
        {
            height = base.GetPropertyHeight(prop, label);
            var content = new GUIContent(helpAttribute.text);
            var style = GUI.skin.GetStyle("helpbox");
            textHeight = style.CalcHeight(content, EditorGUIUtility.currentViewWidth - k_IconSize);

            return height + textHeight + k_Spacing;
        }


        public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, prop);
	        
            var helpPos = position;
            helpPos.height = textHeight;

            EditorGUI.HelpBox(helpPos, helpAttribute.text, (MessageType)helpAttribute.type);
            position.height = height;

            position.y +=  k_Spacing + helpPos.height;
                EditorGUI.PropertyField(position, prop, label);

            EditorGUI.EndProperty();
        }
    }
#endif
}