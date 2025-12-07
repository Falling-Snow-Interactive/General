using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

namespace Fsi.General.Buckets
{
	[CustomPropertyDrawer(typeof(BucketEntry<>), true)]
	public class BucketEntryPropertyDrawer : PropertyDrawer
	{
		public override VisualElement CreatePropertyGUI(SerializedProperty property)
		{
			var root = new VisualElement
			           {
				           style =
				           {
					           flexDirection = FlexDirection.Row,
					           flexGrow = 1
				           }
			           };
			
			SerializedProperty valueProp = property.FindPropertyRelative("value");
			var valueField = new PropertyField(valueProp)
			                 {
				                 label = "",
				                 style =
				                 {
					                 flexGrow = 1
				                 }
			                 };

			SerializedProperty weightProp = property.FindPropertyRelative("weight");
			var weightField = new PropertyField(weightProp)
			                  {
				                  label = "",
				                  style =
				                  {
					                  flexGrow = 0,
					                  width = 50
				                  }
			                  };

			root.Add(valueField);
			root.Add(weightField);

			return root;
		}
	}
}