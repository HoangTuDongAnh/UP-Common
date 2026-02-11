using UnityEditor;
using UnityEngine;
using System.Reflection;
using HoangTuDongAnh.UP.Common.Attributes;

namespace HoangTuDongAnh.UP.Common.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public sealed class ShowIfDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return IsVisible(property)
                ? EditorGUI.GetPropertyHeight(property, label, true)
                : 0f;
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (IsVisible(property))
                EditorGUI.PropertyField(position, property, label, true);
        }

        private bool IsVisible(SerializedProperty property)
        {
            var attr = (ShowIfAttribute)attribute;
            var target = property.serializedObject.targetObject;
            var type = target.GetType();

            var member = type.GetField(attr.ConditionMember,
                             BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                         ?? (MemberInfo)type.GetProperty(attr.ConditionMember,
                             BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            if (member == null) return true;

            object value = member is FieldInfo f ? f.GetValue(target)
                : member is PropertyInfo p ? p.GetValue(target)
                : null;

            return value is bool b && b;
        }
    }
}