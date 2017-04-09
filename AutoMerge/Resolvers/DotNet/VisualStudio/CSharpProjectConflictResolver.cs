using System;
using System.ComponentModel;

namespace AutoMerge.Resolvers.DotNet.VisualStudio
{
    [ResolverForExtension(".csproj")]
    [Description(".csproj both sides added files")]
    public class CSharpProjectConflictResolver : IConflictResolver
    {
        public bool TryResolve(string commonBaseFilename, string leftFilename, string rightFilename, string mergedFilename)
        {
            return false;
        }
    }
}
