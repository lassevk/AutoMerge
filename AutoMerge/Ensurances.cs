using System;

using JetBrains.Annotations;

// ReSharper disable InconsistentNaming

namespace AutoMerge
{
    internal static class Ensurances
    {
        [ContractAnnotation("expression:false => halt")]
        public static void assume(bool expression)
        {
        }

        [NotNull]
        public static T NotNull<T>(this T value) where T : class => value ?? throw new ArgumentNullException(nameof(value));
    }
}
