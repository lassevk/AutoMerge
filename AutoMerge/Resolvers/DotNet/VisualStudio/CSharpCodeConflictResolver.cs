using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

using DiffLib;

using static AutoMerge.Ensurances;

namespace AutoMerge.Resolvers.DotNet.VisualStudio
{
    [ResolverForExtension(".cs")]
    [Description("C# Code File")]
    public class CSharpCodeConflictResolver : TextBasedConflictResolver
    {
        protected override (bool success, string[] resolvedLines) TryResolve(string[] commonLines, string[] leftLines, string[] rightLines)
        {
            bool success = true;
            var result = Merge.Perform(commonLines, leftLines, rightLines, new StringSimilarityDiffElementAligner(), new MethodResolver(resolve)).NotNull().ToArray();
            if (success)
                return (true, result);

            return (false, new string[0]);

            IEnumerable<string> resolve(IList<string> commonBase, IList<string> left, IList<string> right)
            {
                assume(commonBase != null && left != null && right != null);
                var re = new Regex(@"^\s*using\s+[^(]");
                if (commonBase.Count == 0 && left.All(line => re.IsMatch(line.NotNull())) && right.All(line => re.IsMatch(line.NotNull())))
                    return left.Concat(right).Distinct();

                success = false;
                return new string[0];
            }
        }
    }
}