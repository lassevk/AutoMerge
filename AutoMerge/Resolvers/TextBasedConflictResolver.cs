using System;
using System.IO;
using System.Text;

using JetBrains.Annotations;

namespace AutoMerge.Resolvers
{
    public abstract class TextBasedConflictResolver : IConflictResolver
    {
        public bool TryResolve(string commonBaseFilename, string leftFilename, string rightFilename, string mergedFilename)
        {
            var commonLines = File.ReadAllLines(commonBaseFilename);
            var leftLines = File.ReadAllLines(leftFilename);
            var rightLines = File.ReadAllLines(rightFilename);

            var (success, resolvedLines) = TryResolve(commonLines, leftLines, rightLines);
            if (!success)
                return false;

            File.WriteAllLines(mergedFilename, resolvedLines, ResultEncoding);
            return true;
        }

        [NotNull]
        protected virtual Encoding ResultEncoding => Encoding.UTF8;

        protected abstract (bool success, string[] resolvedLines) TryResolve([NotNull] string[] commonLines, [NotNull] string[] leftLines, [NotNull] string[] rightLines);
    }
}
