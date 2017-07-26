using System;
using System.IO;
using System.Text;

using JetBrains.Annotations;

namespace AutoMerge.Resolvers
{
    public abstract class TextBasedFileConflictResolver : IFileConflictResolver
    {
        public bool TryResolve(string commonBaseFilename, string leftFilename, string rightFilename, string mergedFilename)
        {
            var commonLines = File.ReadAllLines(commonBaseFilename);
            var leftLines = File.ReadAllLines(leftFilename);
            var rightLines = File.ReadAllLines(rightFilename);

            var (success, resolvedLines) = TryResolve(Path.GetExtension(mergedFilename), commonLines, leftLines, rightLines);
            if (!success)
                return false;

            File.WriteAllLines(mergedFilename, resolvedLines, ResultEncoding);
            return true;
        }

        public abstract string[] SupportedExtensions { get; }
        public abstract string Description { get; }

        [NotNull]
        protected virtual Encoding ResultEncoding => Encoding.UTF8;

        protected abstract (bool success, string[] resolvedLines) TryResolve([NotNull] string extension, [NotNull] string[] commonLines, [NotNull] string[] leftLines, [NotNull] string[] rightLines);
    }
}
