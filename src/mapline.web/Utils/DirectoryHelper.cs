using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Utils
{
    public static class DirectoryHelper
    {
        public static (int? Start, int? End) ParseYearsFromTheFolderName(string yearFolder)
        {
            if (string.IsNullOrEmpty(yearFolder))
            { 
                throw new ArgumentException("The name of the year folder is null or empty.", nameof(yearFolder));
            }

            var yearArray = yearFolder.Split('-');
            
            if(yearArray.Length != 2)
            {
                throw new ArgumentException($"Cannot manipulate the string that is composed from the year folder '{yearFolder}'. Remove extra '-' characaters from it.", nameof(yearFolder));
            }
            
            return (Start: YearToInt(yearArray[0]), End: YearToInt(yearArray[1]));
        }

        /// <exception cref="FormatException">$"Unable to convert the value '{year}' into a integer number"</exception>
        public static int YearToInt(string year)
        {
            if(string.IsNullOrEmpty(year))
            {
                throw new ArgumentException("the year is null or empty.", nameof(year));
            }

            if(year.EndsWith("BCE"))
            {
                year = "-" + year.Replace("BCE", "");
            }

            if (year.EndsWith("CE"))
            {
                year = year.Replace("CE", "");
            }

            if(int.TryParse(year, out var result))
            {
                return result;
            }

            throw new FormatException($"Unable to convert the value '{year}' into a integer number");
        }


        public static (string Name, object Value) CreateColumn(string filePath, string folderPart)
        {
            if(string.IsNullOrEmpty(filePath))
            {
                throw new ArgumentException("the filename is null or empty", nameof(filePath));
            }

            if (string.IsNullOrEmpty(folderPart))
            {
                throw new ArgumentException("the filename is null or empty", nameof(folderPart));
            }

            // File.Exists check is not done, because we go in here thro Directory.GetFiles
            var filename = filePath.Replace(folderPart, "").TrimStart('\\');

            var index = filename.LastIndexOf('.');
            var columnName = filename.Substring(0, index);
            var columnType = filename.Substring(index + 1);

            using var reader = new StreamReader(filePath);
            string data = reader.ReadToEnd();

            var columnValue = columnType switch
            {
                "geojson" => DataConverter.StringToGeoJson(data),
                _ => (object)data
            };

            return (columnName, columnValue);
        }
    }
}
