using UnityEngine;

namespace Fsi.Gameplay
{
    public class ShowIfAttribute : PropertyAttribute
    {
        public string ConditionField { get; }
        public object[] CompareValues { get; }
        public bool Or { get; }

        public ShowIfAttribute(string conditionField, object compareValue)
        {
            ConditionField = conditionField;
            CompareValues = new[] { compareValue };
        }

        public ShowIfAttribute(string conditionField, object[] compareValues, bool or)
        {
            ConditionField = conditionField;
            CompareValues = compareValues;
            Or = or;
        }
    }
}