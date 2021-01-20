using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Auxilia.Utilities
{
    public class PathInfo
    {
        private static readonly char[] InvalidPathCharacters = Path.GetInvalidPathChars();
        private static readonly char SeparatorChar = Path.DirectorySeparatorChar;

        public PathInfo(string fullPath)
        {
            fullPath.ThrowIfNullOrEmpty(nameof(fullPath));

            FullPath = fullPath.Replace(Path.AltDirectorySeparatorChar, SeparatorChar)
                .Replace($"{SeparatorChar}{SeparatorChar}", $"{SeparatorChar}");

            Chain = Split(FullPath);
        }
        public PathInfo(params string[] chain)
            : this(Combine(chain))
        {
        }
        public PathInfo(IEnumerable<string> chain)
            : this(Combine(chain))
        {
        }

        public string FullPath { get; }
        public string Name
        {
            get => Chain.Last();
        }
        public string Root
        {
            get => Chain.First();
        }
        public string[] Chain { get; }
        public PathInfo Parent
        {
            get => Chain.Length == 1
                ? null
                : new PathInfo(Chain.Take(Chain.Length - 1));
        }

        public bool IsFile
        {
            get => Path.HasExtension(FullPath);
        }
        public bool IsFolder
        {
            get => !Path.HasExtension(FullPath);
        }

        public PathInfo Create()
        {
            Directory.CreateDirectory(FullPath);
            return this;
        }

        public Result IsPathValid(bool isRelative = false)
        {
            if (isRelative)
            {
                if (FullPath.Contains(Path.VolumeSeparatorChar))
                    return Result.Failed($"Path '{FullPath}' is not relative.");
            }
            else
            {
                string root = new string(FullPath.TakeWhile(c => !c.Equals(SeparatorChar)).ToArray());

                if (!root.EndsWith(Path.VolumeSeparatorChar.ToString()))
                    return new FailedResult($"Root of '{FullPath}' does not end with '{Path.VolumeSeparatorChar}'.");
            }

            if (FullPath.Any(InvalidPathCharacters.Contains))
                return new FailedResult($"Path '{FullPath}' contains illegal characters.");

            return new SuccessfulResult($"Path '{FullPath}' is valid.");
        }

        private static string[] Split(string fullPath, bool reverse = false)
        {
            fullPath.ThrowIfNullOrEmpty(nameof(fullPath));

            string[] chain = fullPath.Split(Path.DirectorySeparatorChar);
            return reverse
                ? chain.Reverse().ToArray()
                : chain;
        }

        private static string Combine(IEnumerable<string> chain)
        {
            chain.ThrowIfNull(nameof(chain));
            return chain.Combine(SeparatorChar);
        }
    }
}
