using UnityEditor;
using UnityEngine.UIElements;

namespace Fsi.General.Math.Ranges
{
    [CustomPropertyDrawer(typeof(Range<>))]
    public class RangePropertyDrawer : PropertyDrawer
    {
        public override VisualElement CreatePropertyGUI(SerializedProperty property)
        {
            return new RangeElement(property.displayName, property);
        }
    }
}