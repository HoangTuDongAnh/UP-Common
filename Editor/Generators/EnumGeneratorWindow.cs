using UnityEditor;
using UnityEngine;
using System.Text;

namespace HoangTuDongAnh.UP.Common.Editor.Generators
{
    public sealed class EnumGeneratorWindow : EditorWindow
    {
        private string _enumName = "MyEnum";
        private string _items = "A\nB\nC";

        [MenuItem("Tools/UP-Common/Generators/Enum Generator")]
        private static void Open()
        {
            GetWindow<EnumGeneratorWindow>("Enum Generator");
        }

        private void OnGUI()
        {
            _enumName = EditorGUILayout.TextField("Enum Name", _enumName);
            EditorGUILayout.LabelField("Items (one per line)");
            _items = EditorGUILayout.TextArea(_items, GUILayout.Height(100));

            if (GUILayout.Button("Generate"))
            {
                Generate();
            }
        }

        private void Generate()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"public enum {_enumName}");
            sb.AppendLine("{");

            foreach (var line in _items.Split('\n'))
            {
                if (!string.IsNullOrWhiteSpace(line))
                    sb.AppendLine($"    {line.Trim()},");
            }

            sb.AppendLine("}");

            EditorGUIUtility.systemCopyBuffer = sb.ToString();
            Debug.Log("Enum copied to clipboard.");
        }
    }
}