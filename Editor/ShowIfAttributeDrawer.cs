using System;
using System.Linq;
using UnityEngine.UIElements;
using UnityEditor;
using UnityEditor.UIElements;
using Object = UnityEngine.Object;

namespace Fsi.Gameplay
{
    [CustomPropertyDrawer(typeof(ShowIfAttribute))]
    public class ShowIfAttributeDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            var root = new VisualElement();

            var showIf = (ShowIfAttribute)attribute;
            var conditionProperty = FindConditionProperty(property, showIf.ConditionField);

            var field = new PropertyField(property);
            root.Add(field);

            void UpdateVisibility()
            {
                if (conditionProperty == null)
                {
                    // If the condition field can't be found, just show the property.
                    root.style.display = DisplayStyle.Flex;
                    return;
                }

                bool shouldShow = EvaluateConditions(conditionProperty, showIf.CompareValues, showIf.Or);
                root.style.display = shouldShow ? DisplayStyle.Flex : DisplayStyle.None;
            }

            if (conditionProperty != null)
            {
                root.TrackPropertyValue(conditionProperty, _ => UpdateVisibility());
            }

            UpdateVisibility();
            return root;
        }

        private SerializedProperty FindConditionProperty(SerializedProperty property, string conditionField)
        {
            // Example property.propertyPath: "steps.Array.data[0].npc"
            string path = property.propertyPath;
            int lastDot = path.LastIndexOf('.');
            if (lastDot < 0)
            {
                // Fallback: try root
                return property.serializedObject.FindProperty(conditionField);
            }

            string containerPath = path.Substring(0, lastDot + 1);
            string fullPath = containerPath + conditionField;
            return property.serializedObject.FindProperty(fullPath);
        }

        private bool EvaluateConditions(SerializedProperty conditionProperty, object[] compareValues, bool or)
        {
            return or ? compareValues.Any(c => EvaluateCondition(conditionProperty, c)) 
                       : compareValues.All(c => EvaluateCondition(conditionProperty, c));
        }

        private static bool EvaluateCondition(SerializedProperty conditionProperty, object compareValue)
        {
            switch (conditionProperty.propertyType)
            {
                case SerializedPropertyType.Generic:
                case SerializedPropertyType.ObjectReference:
                    return conditionProperty.objectReferenceValue == (Object)compareValue;
                case SerializedPropertyType.Boolean:
                    switch (compareValue)
                    {
                        case null:
                            return conditionProperty.boolValue;
                        case bool b:
                            return conditionProperty.boolValue == b;
                    }
                    break;
                case SerializedPropertyType.Enum:
                {
                    int targetIndex = -1;

                    switch (compareValue)
                    {
                        case null:
                            return false;
                        case int i:
                            targetIndex = i;
                            break;
                        case string enumName:
                        {
                            string[] names = conditionProperty.enumNames;
                            targetIndex = System.Array.IndexOf(names, enumName);
                            break;
                        }
                        default:
                        {
                            if (compareValue.GetType().IsEnum)
                            {
                                // [ShowIf(nameof(stepType), StepType.NPC)]
                                string targetName = compareValue.ToString();
                                string[] names = conditionProperty.enumNames;
                                targetIndex = System.Array.IndexOf(names, targetName);
                            }

                            break;
                        }
                    }

                    if (targetIndex >= 0)
                    {
                        return conditionProperty.enumValueIndex == targetIndex;
                    }
                    break;
                }
                case SerializedPropertyType.Integer:
                    if (compareValue is int i2)
                    {
                        return conditionProperty.intValue == i2;
                    }
                    break;
                case SerializedPropertyType.Float:
                    if (compareValue is float f)
                    {
                        return Math.Abs(conditionProperty.floatValue - f) < 0.0001f;
                    }
                    break;
                case SerializedPropertyType.String:
                    if (compareValue is string s2)
                    {
                        return conditionProperty.stringValue == s2;
                    }
                    break;
                case SerializedPropertyType.Color:
                case SerializedPropertyType.LayerMask:
                case SerializedPropertyType.Vector2:
                case SerializedPropertyType.Vector3:
                case SerializedPropertyType.Vector4:
                case SerializedPropertyType.Rect:
                case SerializedPropertyType.ArraySize:
                case SerializedPropertyType.Character:
                case SerializedPropertyType.AnimationCurve:
                case SerializedPropertyType.Bounds:
                case SerializedPropertyType.Gradient:
                case SerializedPropertyType.Quaternion:
                case SerializedPropertyType.ExposedReference:
                case SerializedPropertyType.FixedBufferSize:
                case SerializedPropertyType.Vector2Int:
                case SerializedPropertyType.Vector3Int:
                case SerializedPropertyType.RectInt:
                case SerializedPropertyType.BoundsInt:
                case SerializedPropertyType.ManagedReference:
                case SerializedPropertyType.Hash128:
                case SerializedPropertyType.RenderingLayerMask:
                case SerializedPropertyType.EntityId:
                default:
                    throw new ArgumentOutOfRangeException();
            }

            return false;
        }
    }
}