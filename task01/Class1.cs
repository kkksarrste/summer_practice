using System;
using System.Linq;

namespace task01;

public static class StringExtensions
{
    public static bool IsPalindrome(this string input)
    {
        if (input == null || input.Length == 0)
            return false;

        var cleaned = new string(input
            .ToLowerInvariant()
            .Where(c => !char.IsPunctuation(c) && !char.IsWhiteSpace(c))
            .ToArray());

        if (cleaned.Length == 0)
            return false;

        return cleaned.SequenceEqual(cleaned.Reverse());
    }
}
