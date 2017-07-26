using System;
using System.ComponentModel;
using System.IO;

using DiffLib;

namespace AutoMerge.Resolvers
{
    public class TextFileFileConflictResolver : IFileConflictResolver
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

        public string[] SupportedExtensions => new[] { ".txt" };
        public string Description => ".txt dummy resolver";
    }
}
