using System;
using System.Collections.Generic;

using JetBrains.Annotations;

namespace AutoMerge
{
    [UsedImplicitly]
    public class ResolverFactory<T>
        where T : IFileRelatedConflictResolver
    {
        [NotNull]
        private static readonly Dictionary<string, List<T>> _Resolvers = new Dictionary<string, List<T>>();

        static ResolverFactory()
        {
            Type intf = typeof(T);
            foreach (Type type in typeof(FileResolverFactory).Assembly.GetTypes())
            {
                if (type == null)
                    continue;
                if (!intf.IsAssignableFrom(type))
                    continue;
                if (type.IsAbstract)
                    continue;

                var instance = (T)Activator.CreateInstance(type).NotNull();
                foreach (var extension in instance.SupportedExtensions)
                {
                    if (!_Resolvers.TryGetValue(extension, out var resolversForExtension))
                    {
                        resolversForExtension = new List<T>();
                        _Resolvers[extension] = resolversForExtension;
                    }
                    resolversForExtension.Add(instance);
                }
            }
        }

        [NotNull, ItemNotNull]
        public static List<T> GetResolverForExtension([NotNull] string extension)
        {
            if (extension == null)
                throw new ArgumentNullException(nameof(extension));

            List<T> resolversForExtension;
            if (!_Resolvers.TryGetValue(extension, out resolversForExtension))
                resolversForExtension = new List<T>();
            return resolversForExtension;
        }
    }
}