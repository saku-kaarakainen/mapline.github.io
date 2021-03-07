using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using Mapline.Web.Data;
using Mapline.Web.Models;
using NetTopologySuite.Features;

namespace Mapline.Web.Utils
{
    public interface IDirectory
    {
        IEnumerable<string> GetDirectories(string path);
        IEnumerable<string> GetFiles(string path);
    }

    public class DirectoryHelper : IDirectory
    {
        public static async Task WriteAllTextIfDataAsync(string path, string text)
        {
            if (!string.IsNullOrEmpty(text))
            {
                await File.WriteAllTextAsync(path, text);
                // TODO: Add log, data written
            }
            else
            {
                // TODO: Add log. data not written.
            }
        }

        public readonly bool unifySlashes;
        public readonly string pathSeparatorCharacter;

        public DirectoryHelper(string pathSeparatorCharacter = "\\", bool unifySlashes = true)
        {
            if(pathSeparatorCharacter != "\\" && pathSeparatorCharacter != "/")
            {
                throw new InvalidOperationException($"Unsupported path separator character '{pathSeparatorCharacter}'.");
            }

            this.pathSeparatorCharacter = pathSeparatorCharacter;
            this.unifySlashes = unifySlashes;
        }

        private IEnumerable<string> Unify(params string[] path)
        {
            if (!this.unifySlashes)
            {
                // No unifying
                return path;
            }

            if (this.pathSeparatorCharacter == "\\")
            {
                // windows \
                return path.Select(p => p.Replace("/", "\\"));
            }
            else if (this.pathSeparatorCharacter == "/")
            {
                // Unix = /
                return path.Select(p => p.Replace("\\", "//"));
            }

            throw new ApplicationException($"Unexpected code line due to the character '{this.pathSeparatorCharacter}'.");
        }

        public IEnumerable<string> GetDirectories(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("the path is null or empty.", nameof(path));
            }

            return Unify(Directory.GetDirectories(path));
        }

        public IEnumerable<string> GetFiles(string path)
        {
            if (string.IsNullOrEmpty(path))
            {
                throw new ArgumentException("the path is null or empty.", nameof(path));
            }

            return Unify(Directory.GetFiles(path));
        }
    }
}
