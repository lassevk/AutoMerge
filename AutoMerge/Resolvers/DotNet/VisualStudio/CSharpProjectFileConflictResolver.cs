using System;
using System.ComponentModel;

namespace AutoMerge.Resolvers.DotNet.VisualStudio
{
    public class CSharpProjectFileConflictResolver : IFileConflictResolver
    {
        public bool TryResolve(string commonBaseFilename, string leftFilename, string rightFilename, string mergedFilename)
        {
            return false;
        }

        public string[] SupportedExtensions => new[] { ".csproj" };
        public string Description => "C# Project File";
    }
}
