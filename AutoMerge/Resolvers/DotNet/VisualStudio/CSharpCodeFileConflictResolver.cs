using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text.RegularExpressions;

using DiffLib;

using static AutoMerge.Ensurances;

namespace AutoMerge.Resolvers.DotNet.VisualStudio
{
    public class CSharpCodeFileConflictResolver : TextFragmentBasedFileConflictResolver
    {
        public override string[] SupportedExtensions => new[] { ".cs" };
        public override string Description => "C# Code File";
    }

    public class CSharpUsingStatementsInTheSamePlaceFragmentConflictResolver : IFragmentConflictResolver
    {
        public IEnumerable<string> TryResolve(IList<string> commonBase, IList<string> left, IList<string> right)
        {
            var re = new Regex(@"^\s*using\s+[^(]");
            if (commonBase.Count == 0 && left.All(line => re.IsMatch(line.NotNull())) && right.All(line => re.IsMatch(line.NotNull())))
                return left.Concat(right).Distinct();

            return null;
        }

        public string[] SupportedExtensions => new[] { ".cs" };
        public string Description => "using statements in the same place";
    }
}