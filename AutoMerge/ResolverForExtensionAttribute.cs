using System;

using JetBrains.Annotations;

namespace AutoMerge
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = false)]
    public class ResolverForExtensionAttribute : Attribute
    {
        public ResolverForExtensionAttribute([NotNull] string extension)
        {
            Extension = extension ?? throw new ArgumentNullException(nameof(extension));
        }

        [NotNull]
        public string Extension { get; }
    }
}