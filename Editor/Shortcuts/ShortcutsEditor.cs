using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace Fsi.General.Shortcuts
{
    public sealed class ShortcutsEditor : EditorWindow
    {
        private const string StyleSheetPath = "Packages/com.fallingsnowinteractive.general/Editor/Shortcuts/ShortcutsEditor.uss";
        
        private readonly List<MethodInfo> methods = new();

        private Toolbar toolbar;

        [MenuItem("FSI/Tools/Shortcut Toolbar")]
        public static void OpenWindow()
        {
            ShortcutsEditor window = GetWindow<ShortcutsEditor>();
            window.titleContent = new GUIContent("Shortcuts");
            window.Show();
        }

        public void CreateGUI()
        {
            rootVisualElement.Clear();
            rootVisualElement.AddToClassList("shortcuts-editor");

            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath);
            if (styleSheet != null)
            {
                rootVisualElement.styleSheets.Add(styleSheet);
            }
            
            rootVisualElement.Clear();
            
            toolbar = new Toolbar();
            toolbar.AddToClassList("shortcuts-toolbar");
            rootVisualElement.Add(toolbar);

            BuildToolbar();
        }

        private void BuildToolbar()
        {
            toolbar.Clear();
            
            RefreshShortcuts();

            ToolbarSpacer s = new();
            s.AddToClassList("shortcuts-toolbar__spacer");
            toolbar.Add(s);
            
            ToolbarButton refreshButton = new(BuildToolbar) { text = "Refresh" };
            Texture refreshIcon = EditorGUIUtility.IconContent("Refresh").image;
            if (refreshIcon != null)
            {
                Image refreshImage = new()
                                     {
                                         image = refreshIcon,
                                         scaleMode = ScaleMode.ScaleToFit
                                     };
                refreshImage.AddToClassList("shortcuts-toolbar__icon");
                refreshButton.Insert(0, refreshImage);
            }
            refreshButton.AddToClassList("shortcuts-toolbar__refresh");
            toolbar.Add(refreshButton);
        }

        private void RefreshShortcuts()
        {
            methods.Clear();
            methods.AddRange(TypeCache.GetMethodsWithAttribute<ShortcutAttribute>());

            // Sort by ShortcutAttribute.Priority (ascending, lower = earlier), then alphabetically
            List<MethodInfo> sorted = methods
                                      .OrderBy(m => m.GetCustomAttribute<ShortcutAttribute>()?.Priority ?? 0)
                                      .ThenBy(m => m.Name, System.StringComparer.OrdinalIgnoreCase)
                                      .ToList();

            methods.Clear();
            methods.AddRange(sorted);

            RebuildShortcutList();
        }

        private void RebuildShortcutList()
        {
            // Root toolbar menus by name
            Dictionary<string, ToolbarMenu> toolbarMenus = new();

            foreach (MethodInfo method in methods)
            {
                if (!ValidateShortcutMethod(method))
                {
                    continue;
                }

                ShortcutAttribute attribute = method.GetCustomAttribute<ShortcutAttribute>();
                string label = string.IsNullOrWhiteSpace(attribute?.Name)
                                   ? $"{method.DeclaringType?.Name ?? "<unknown>"}/{method.Name}"
                                   : attribute.Name;

                // If the label contains "/", treat it as a ToolbarMenu path (e.g. "Test/L1/L2/Testing")
                if (label.Contains("/"))
                {
                    string[] parts = label.Split('/');

                    string rootMenuName = parts[0];
                    // Everything after the root becomes the nested submenu path.
                    // Unity's DropdownMenu supports nested submenus by using "/" in the action name.
                    string itemPath = parts.Length > 1
                                          ? string.Join("/", parts.Skip(1))
                                          : method.Name;

                    if (!toolbarMenus.TryGetValue(rootMenuName, out ToolbarMenu menu))
                    {
                        menu = new ToolbarMenu { text = rootMenuName };
                        menu.AddToClassList("shortcuts-toolbar__menu");
                        AddIcon(menu, ResolveShortcutIcon(attribute));
                        toolbarMenus[rootMenuName] = menu;
                        toolbar?.Add(menu);
                    }

                    menu.menu.AppendAction(itemPath, _ => method.Invoke(null, null));

                    continue;
                }

                // Otherwise, draw as a normal toolbar button
                ToolbarButton shortcutButton = new(clickEvent: () => method.Invoke(null, null))
                                               {
                                                   text = label
                                               };

                shortcutButton.AddToClassList("shortcuts-toolbar__button");
                AddIcon(shortcutButton, ResolveShortcutIcon(attribute));
                toolbar?.Add(shortcutButton);
            }
        }

        private static Texture2D ResolveShortcutIcon(ShortcutAttribute attribute)
        {
            string icon = attribute?.Icon?.Trim();
            if (string.IsNullOrEmpty(icon))
            {
                return null;
            }

            Texture2D texture = EditorGUIUtility.IconContent(icon).image as Texture2D;
            return texture != null ? texture : AssetDatabase.LoadAssetAtPath<Texture2D>(icon);
        }

        private static void AddIcon(VisualElement element, Texture2D texture)
        {
            if (element == null || texture == null)
            {
                return;
            }

            Image icon = new() { image = texture };
            icon.AddToClassList("shortcuts-toolbar__icon");
            element.Insert(0, icon);
        }

        private static bool ValidateShortcutMethod(MethodInfo method)
        {
            if (method == null)
            {
                Debug.LogWarning("Shortcut validation failed: method is null.");
                return false;
            }

            bool isValid = method.IsStatic
                           && method.GetParameters().Length == 0
                           && method.ReturnType == typeof(void);

            if (!isValid)
            {
                string declaringTypeName = method.DeclaringType?.FullName ?? "<unknown>";
                Debug.LogWarning($"Invalid shortcut method '{declaringTypeName}.{method.Name}'. " +
                                 "Shortcuts must be static, parameterless, and return void.");
            }

            return isValid;
        }
    }
}
