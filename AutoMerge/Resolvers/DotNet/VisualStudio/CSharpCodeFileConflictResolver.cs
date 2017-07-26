using System;
using System.ComponentModel;

using DiffLib;

using static AutoMerge.Ensurances;

namespace AutoMerge.Resolvers.DotNet.VisualStudio
{
    public class CSharpCodeFileConflictResolver : TextFragmentBasedFileConflictResolver
    {
        public override string[] SupportedExtensions => new[] { ".cs" };
        public override string Description => "C# Code File";
    }
}