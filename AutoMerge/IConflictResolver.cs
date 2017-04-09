using System;

using JetBrains.Annotations;

namespace AutoMerge
{
    public interface IConflictResolver
    {
        bool TryResolve([NotNull] string commonBaseFilename, [NotNull] string leftFilename, [NotNull] string rightFilename, [NotNull] string mergedFilename);
    }
}
