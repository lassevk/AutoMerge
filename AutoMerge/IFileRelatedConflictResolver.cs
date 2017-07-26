using System;

using JetBrains.Annotations;

namespace AutoMerge
{
    [UsedImplicitly]
    public interface IFileRelatedConflictResolver
    {
        [NotNull, ItemNotNull]
        string[] SupportedExtensions { get; }

        [NotNull]
        string Description { get; }
    }
}