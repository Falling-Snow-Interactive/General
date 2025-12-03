#if UNITY_EDITOR
using System.Collections.Generic;
using System.Linq;
using Fsi.General.GridArrays;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.GridArray
{
    // Works for all EnumGrid<TEnum> specializations
    [CustomPropertyDrawer(typeof(EnumGrid<>), true)]
    public class EnumGridDrawer : GridArrayDrawer<int>
    {
        protected override string StyleSheetPath => "Packages/com.fallingsnowinteractive.general/Editor/GridArray/EnumGridDrawer.uss";
        protected override float CellWidth => 60f;

        protected override VisualElement CreateCell(SerializedProperty elementProp)
        {
            // elementProp is an enum-typed SerializedProperty here

            // Get the names and current index from the enum property
            List<string> displayNames = elementProp.enumDisplayNames.ToList();
            int currentIndex = Mathf.Clamp(elementProp.enumValueIndex, 0, displayNames.Count - 1);
            
            PopupField<string> popup = new PopupField<string>(displayNames, currentIndex)
                                       {
                                           style =
                                           {
                                               flexGrow = 0,
                                               flexShrink = 0,
                                               width = CellWidth,
                                               height = EditorGUIUtility.singleLineHeight,
                                           },
                                       };

            popup.AddToClassList("enum-grid__cell");

            // Sync UI -> SerializedProperty
            popup.RegisterValueChangedCallback(_ =>
                                               {
                                                   elementProp.enumValueIndex = popup.index;
                                                   elementProp.serializedObject.ApplyModifiedProperties();
                                               });

            return popup;
        }
    }
}
#endif