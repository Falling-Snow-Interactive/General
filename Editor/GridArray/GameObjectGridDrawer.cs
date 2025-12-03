#if UNITY_EDITOR
using Fsi.General.GridArrays;
using UnityEditor;
using UnityEditor.Search;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.GridArray
{
    [CustomPropertyDrawer(typeof(GameObjectGrid))]
    public class GameObjectGridDrawer : GridArrayDrawer<GameObject>
    {
        protected override string StyleSheetPath => "Packages/com.fallingsnowinteractive.general/Editor/GridArray/GameObjectGridDrawer.uss";
        protected override float CellWidth => 50f;
        
        protected override VisualElement CreateCell(SerializedProperty elementProp)
        {
            // Create a small ObjectField that holds a GameObject reference
            var objectField = new ObjectField
                              {
                                  objectType = typeof(GameObject),
                                  // allowSceneObjects = true,
                                  style =
                                  {
                                      flexGrow = 1,
                                      flexShrink = 0,
                                      
                                      width = CellWidth,
                                      height = EditorGUIUtility.singleLineHeight,
                                      // minHeight = MinWidth,
                                  }
                              };

            // Base USS class for all cells
            objectField.AddToClassList("gameobject-grid__cell");

            // Initialize from the current value
            objectField.value = elementProp.objectReferenceValue as GameObject;

            // Sync UI -> SerializedProperty
            objectField.RegisterValueChangedCallback(evt =>
                                                     {
                                                         elementProp.objectReferenceValue = evt.newValue as GameObject;
                                                         elementProp.serializedObject.ApplyModifiedProperties();
                                                     });

            return objectField;
        }
    }
}
#endif