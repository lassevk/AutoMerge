using System;
using System.ComponentModel;
using System.IO;

using DiffLib;

namespace AutoMerge.Resolvers
{
    [ResolverForExtension(".txt")]
    [Description(".txt dummy resolver")]
    public class TextFileConflictResolver : IConflictResolver
    {
        public bool TryResolve(string commonBaseFilename, string leftFilename, string rightFilename, string mergedFilename)
        {
            var commonBase = File.ReadAllLines(commonBaseFilename);
            var left = File.ReadAllLines(leftFilename);
            var right = File.ReadAllLines(rightFilename);
            try
            {
                var merged = Merge.Perform(commonBase, left, right, new StringSimilarityDiffElementAligner(), new TakeLeftThenRightIfRightDiffersFromLeft<string>());
                File.WriteAllLines(mergedFilename, merged);
                return true;
            }
            catch (MergeConflictException)
            {
                return false;
            }
        }
    }
}
