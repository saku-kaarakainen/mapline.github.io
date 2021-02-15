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
    public static class DirectoryHelper
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
    }
}
