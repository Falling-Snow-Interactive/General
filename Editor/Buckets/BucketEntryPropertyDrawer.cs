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
			VisualElement root = new()
			                     {
				                     style =
				                     {
					                     flexDirection = FlexDirection.Row,
					                     flexGrow = 1
				                     }
			                     };
			
			SerializedProperty weightProp = property.FindPropertyRelative("weight");
			PropertyField weightField = new(weightProp)
			                            {
				                            label = "",
				                            style =
				                            {
					                            flexGrow = 0,
					                            width = 50
				                            }
			                            };
			
			SerializedProperty valueProp = property.FindPropertyRelative("value");
			PropertyField valueField = new(valueProp)
			                           {
				                           label = "",
				                           style =
				                           {
					                           flexGrow = 1
				                           }
			                           };

			root.Add(weightField);
			root.Add(valueField);

			return root;
		}
	}
}