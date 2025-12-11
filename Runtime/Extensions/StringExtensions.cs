// Copyright Falling Snow Interactive 2025

using System;

namespace Fsi.General.Extensions
{
    public static class StringExtensions
    {
        public static string ToSingular(this string word)
        {
            if (string.IsNullOrWhiteSpace(word))
                return word;

            // Ends with "ies" → replace with "y"
            if (word.EndsWith("ies", StringComparison.OrdinalIgnoreCase))
                return word[..^3] + "y";

            // Ends with "ses" or "xes" or similar → remove only 'es'
            if (word.EndsWith("es", StringComparison.OrdinalIgnoreCase) &&
                !word.EndsWith("ses", StringComparison.OrdinalIgnoreCase) &&
                !word.EndsWith("xes", StringComparison.OrdinalIgnoreCase))
                return word[..^2];

            // Ends with "s" → remove final s
            if (word.EndsWith("s", StringComparison.OrdinalIgnoreCase))
                return word[..^1];

            return word;
        }
    }
}