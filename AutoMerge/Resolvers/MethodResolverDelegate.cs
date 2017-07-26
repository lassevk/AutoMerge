using System;
using System.Collections.Generic;

namespace AutoMerge.Resolvers
{
    public delegate IEnumerable<string> MethodResolverDelegate(IList<string> commonBase, IList<string> left, IList<string> right);
}