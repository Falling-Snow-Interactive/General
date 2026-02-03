using System;
using JetBrains.Annotations;

namespace Fsi.General.Shortcuts
{
    /// <summary>
    /// Marks a method as discoverable editor shortcut.
    /// </summary>
    /// <remarks>
    /// Discovery contract: methods must be static, parameterless, and return void.
    /// </remarks>
    [UsedImplicitly, MeansImplicitUse]
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public sealed class ShortcutAttribute : Attribute
    {
        public string Name { get; }
        public int Priority { get; }
        public string Icon { get; set; }

        public ShortcutAttribute()
        {
            Name = "";
            Priority = 0;
            Icon = "";
        }
        
        public ShortcutAttribute(string name = "", int priority = 0, string icon = "")
        {
            Name = name;
            Priority = priority;
            Icon = icon ?? "";
        }
    }
}
