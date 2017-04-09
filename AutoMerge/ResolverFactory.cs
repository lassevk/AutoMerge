using System;
using System.Collections.Generic;
using System.Reflection;

using JetBrains.Annotations;

namespace AutoMerge
{
    public static class ResolverFactory
    {
        [NotNull]
        private static readonly Dictionary<string, List<IConflictResolver>> _Resolvers = new Dictionary<string, List<IConflictResolver>>();

        static ResolverFactory()
        {
            Type intf = typeof(IConflictResolver);
            foreach (Type type in typeof(ResolverFactory).Assembly.GetTypes())
            {
                if (type == null)
                    continue;
                if (!intf.IsAssignableFrom(type))
                    continue;
                if (type.IsAbstract)
                    continue;

                foreach (var attr in type.GetCustomAttributes<ResolverForExtensionAttribute>(true) ?? new List<ResolverForExtensionAttribute>())
                {
                    var extension = attr?.Extension;
                    if (extension == null)
                        continue;
                    var instance = (IConflictResolver)Activator.CreateInstance(type);

                    if (!_Resolvers.TryGetValue(extension, out var resolversForExtension))
                    {
                        resolversForExtension = new List<IConflictResolver>();
                        _Resolvers[extension] = resolversForExtension;
                    }
                    resolversForExtension.Add(instance);
                }
            }
        }

        [NotNull, ItemNotNull]
        public static List<IConflictResolver> GetResolverForExtension([NotNull] string extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            List<IConflictResolver> resolversForExtension;
            if (!_Resolvers.TryGetValue(extension, out resolversForExtension))
                resolversForExtension = new List<IConflictResolver>();
            return resolversForExtension;
        }
    }
}
