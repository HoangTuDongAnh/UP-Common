using HoangTuDongAnh.UP.Common.Attributes;
using UnityEditor;
using UnityEngine;

namespace HoangTuDongAnh.UP.Common.Editor.PropertyDrawers
{
    [CustomPropertyDrawer(typeof(MinMaxAttribute))]
    public sealed class MinMaxDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.Vector2)
            {
                EditorGUI.PropertyField(position, property, label);
                return;
            }

            var attr = (MinMaxAttribute)attribute;
            var v = property.vector2Value;

            EditorGUI.BeginProperty(position, label, property);
            EditorGUI.MinMaxSlider(position, label, ref v.x, ref v.y, attr.Min, attr.Max);
            property.vector2Value = v;
            EditorGUI.EndProperty();
        }
    }
}