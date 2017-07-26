using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;

namespace AutoMerge
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args?.Length != 4)
            {
                Console.Error?.WriteLine("Must be invoked with $LOCAL $REMOTE $BASE $MERGED");
                Environment.Exit(1);
            }

            var localFilename = args[0] ?? string.Empty;
            var remoteFilename = args[1] ?? string.Empty;
            var baseFilename = args[2] ?? string.Empty;
            var mergedFilename = args[3] ?? string.Empty;

            Console.WriteLine($"automerge.base: {baseFilename}");
            Console.WriteLine($"automerge.local: {localFilename}");
            Console.WriteLine($"automerge.remote: {remoteFilename}");
            Console.WriteLine($"automerge.output: {mergedFilename}");

            var extension = Path.GetExtension(mergedFilename).ToLowerInvariant();
            var resolvers = FileResolverFactory.GetResolverForExtension(extension);
            if (!resolvers.Any())
            {
                Console.Error?.WriteLine($"automerge.error: no automatic merge rules found for file extension {extension}");
                Environment.Exit(1);
            }

            foreach (var resolver in resolvers)
            {
                if (resolver.TryResolve(baseFilename, localFilename, remoteFilename, mergedFilename))
                {
                    var attr = resolver.GetType().GetCustomAttribute<DescriptionAttribute>(false);
                    if (attr != null)
                        Console.WriteLine($"automerge: conflict resolved using '{attr.Description}' conflict resolver");
                    Console.WriteLine("automerge.success: marking file as successfully merged");
                    Environment.Exit(0);
                }
            }

            Console.Error?.WriteLine($"automerge.error: no automatic merge rules for file extension {extension} was able to merge the file");
            Environment.Exit(1);
        }
    }
}
