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
        public string IconNameOrPath { get; }

        public ShortcutAttribute()
        {
            Name = "";
            Priority = 0;
            IconNameOrPath = null;
        }
        
        public ShortcutAttribute(string name = "", int priority = 0)
        {
            Name = name;
            Priority = priority;
            IconNameOrPath = null;
        }

        public ShortcutAttribute(string name, int priority, string iconNameOrPath)
        {
            Name = name;
            Priority = priority;
            IconNameOrPath = iconNameOrPath;
        }
    }
}
