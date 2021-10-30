using Auxilia.Extensions;
using System;
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
            FullPath = fullPath?.Replace(Path.AltDirectorySeparatorChar, SeparatorChar)
                .Replace($"{SeparatorChar}{SeparatorChar}", $"{SeparatorChar}");
        }
        public PathInfo(params string[] chain)
            : this(Combine(chain))
        {
        }
        public PathInfo(IEnumerable<string> chain)
            : this(Combine(chain))
        {
        }

        public string FullPath { get; private set; }
        public string[] Chain
		{
            get => Split(FullPath);
		}
        public string Name
        {
            get => Chain.Last();
        }
        public string NameWithoutExtension
        {
            get => Name.Left(Name.Length - Extension.Length);
        }
        public string Extension
        {
            get => Path.GetExtension(FullPath);
        }
        public string Directory
        {
            get => Parent?.FullPath ?? string.Empty;
        }
        public string Root
        {
            get => Chain.First();
        }
        public PathInfo Parent
        {
            get => Chain.Length == 1
                ? null
                : new PathInfo(Chain.Take(Chain.Length - 1));
        }
        public bool IsRooted
		{
            get => Path.IsPathRooted(FullPath);
		}

        public bool IsEmpty
        {
            get => string.IsNullOrWhiteSpace(FullPath);
        }
        public bool IsFile
        {
            get => Path.HasExtension(FullPath);
        }
        public bool IsFolder
        {
            get => !IsEmpty && !Path.HasExtension(FullPath);
        }

        public bool Exists
        {
            get => IsFile
                ? File.Exists(FullPath)
                : System.IO.Directory.Exists(FullPath);
        }

        public PathInfo Create(bool hidden = false)
        {
            if(IsFile)
            {
                File.Create(FullPath);

                if (hidden)
                    File.SetAttributes(FullPath, FileAttributes.Hidden);
            }
            else if(IsFolder)
            {
                DirectoryInfo directoryInfo = System.IO.Directory.CreateDirectory(FullPath);

                if (hidden)
                    directoryInfo.Attributes = FileAttributes.Directory | FileAttributes.Hidden;
            }
            else
            {
                throw new InvalidOperationException("Cannot create when path is null empty or whitespace.");
            }

            return this;
        }
        public void Delete()
        {
            if (!Exists)
                return;
            
            if (IsFile)
                File.Delete(FullPath);
            else
                System.IO.Directory.Delete(FullPath);
        }

        public bool IsValid(bool isRelative = false)
        {
            if (IsEmpty)
		        return false;
                //return Result.Failed("Path is null or empty.");

            if (isRelative)
            {
	            if (FullPath.Contains(Path.VolumeSeparatorChar))
		            return false;
	            //return Result.Failed($"Path '{FullPath}' is not relative.");
            }
            else
            {
                string root = new string(FullPath.TakeWhile(c => !c.Equals(SeparatorChar)).ToArray());

                if (!root.EndsWith(Path.VolumeSeparatorChar.ToString()))
			        return false;
                    //return Result.Failed($"Root of '{FullPath}' does not end with '{Path.VolumeSeparatorChar}'.");
            }

            return !FullPath.Any(InvalidPathCharacters.Contains);
            //? Result.Failed($"Path '{FullPath}' contains illegal characters.")
            //: Result.Successful($"Path '{FullPath}' is valid.");
        }

        public PathInfo ChangeExtension(string extension)
		{
            if(!Chain.Any() || IsRooted && Chain.Length == 1)
                throw new InvalidOperationException($"Cannot change path of \"{FullPath}\".");

            FullPath = Path.ChangeExtension(FullPath, extension);
            
            return this;
		}

        public override string ToString()
        {
	        return FullPath;
        }

        private static string[] Split(string fullPath)
        {
            return string.IsNullOrWhiteSpace(fullPath) 
                ? Array.Empty<string>() 
                : fullPath.Split(Path.DirectorySeparatorChar);
        }

        private static string Combine(IEnumerable<string> chain)
        {
            chain.ThrowIfNull(nameof(chain));
            return chain.Combine(SeparatorChar);
        }
    }
}
