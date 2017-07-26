using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace AutoMerge.Resolvers
{
    public interface IFragmentConflictResolver : IFileRelatedConflictResolver
    {
        [CanBeNull]
        IEnumerable<string> TryResolve([NotNull] IList<string> commonBase, [NotNull] IList<string> left, [NotNull] IList<string> right);
    }
}