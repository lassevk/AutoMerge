using System;
using System.Collections.Generic;
using System.Linq;

using DiffLib;

using static AutoMerge.Ensurances;

namespace AutoMerge.Resolvers
{
    // [ResolverForExtension(".cs")]
    public abstract class TextFragmentBasedFileConflictResolver : TextBasedFileConflictResolver
    {
        protected override (bool success, string[] resolvedLines) TryResolve(string extension, string[] commonLines, string[] leftLines, string[] rightLines)
        {
            bool success = true;
            var result = Merge.Perform(commonLines, leftLines, rightLines, new StringSimilarityDiffElementAligner(), new MethodResolver(resolve)).NotNull().ToArray();
            if (success)
                return (true, result);

            return (false, new string[0]);

            IEnumerable<string> resolve(IList<string> commonBase, IList<string> left, IList<string> right)
            {
                assume(commonBase != null && left != null && right != null);

                foreach (var resolver in FragmentResolverFactory.GetResolverForExtension(extension))
                {
                    var resolvedResult = resolver.TryResolve(commonBase, left, right);
                    if (resolvedResult != null)
                    {
                        Console.WriteLine($"automerge.info: fragment resolved {Description}: {resolver.Description}");
                        return resolvedResult;
                    }
                }

                success = false;
                return new string[0];
            }
        }
    }
}