using System;
using System.Collections.Generic;

using DiffLib;

using JetBrains.Annotations;

namespace AutoMerge.Resolvers
{
    public class MethodResolver : IMergeConflictResolver<string>
    {
        [NotNull]
        private readonly MethodResolverDelegate _Method;

        public MethodResolver([NotNull] MethodResolverDelegate method)
        {
            _Method = method ?? throw new ArgumentNullException(nameof(method));
        }

        public IEnumerable<string> Resolve(IList<string> commonBase, IList<string> left, IList<string> right)
        {
            return _Method(commonBase, left, right);
        }
    }
}
