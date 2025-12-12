using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.General.Buckets
{
	[CustomPropertyDrawer(typeof(Bucket<,>), true)]
	public class BucketDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			VisualElement root = new();

			SerializedProperty entriesProp = property.FindPropertyRelative("entries");
			PropertyField entriesField = new(entriesProp){label = property.displayName};
			root.Add(entriesField);
			
			return root;
		}
	}
}