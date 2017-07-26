using System;

using AutoMerge.Resolvers;

using JetBrains.Annotations;

namespace AutoMerge
{
    public interface IFileConflictResolver : IFileRelatedConflictResolver
    {
        bool TryResolve([NotNull] string commonBaseFilename, [NotNull] string leftFilename, [NotNull] string rightFilename, [NotNull] string mergedFilename);
    }
}