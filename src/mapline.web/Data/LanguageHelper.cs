using Mapline.Web.Models;
using Mapline.Web.Utils;
using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public interface ILanguageHelper
    {
        string LanguagePathFolder { get; set; }

        Task SaveLanguageToFile(SaveLanguageModel language);
    }

    public class LanguageHelper : ILanguageHelper
    {
        #region Singleton
        private static Lazy<LanguageHelper> lazyInstance = new Lazy<LanguageHelper>(() => new LanguageHelper());

        public static LanguageHelper Instance => lazyInstance.Value;
        #endregion

        public string LanguagePathFolder { get; set; } = "./../../data/Language/";

        public async Task SaveLanguageToFile(SaveLanguageModel language)
        {
            // Save to:
            // {path-to-project}/data/Language/{language.Name}/{language.YearStart}-{language.YearEnd}
            //      - area.geojson: language.Area
            //      - features.json: language.Features
            //      - additionalDetails.json: language.AdditionalDetails
            var years = YearsToFolderName(language.YearStart.Value, language.YearEnd.Value);
            var path = $"{LanguagePathFolder}/{language.Name}/{years}/";

            Directory.CreateDirectory(path);

            if (File.Exists($"{path}/area.geojson"))
            {
                throw new InvalidOperationException($"Save cannot be done, because the geojson already exists in the given name / year range '{language.Name}/{years}/'");
            }

            var geojson = EnsureDataIsFeatureCollectiion(language.GeoJson.ToString());

            await DirectoryHelper.WriteAllTextIfDataAsync($"{path}/area.geojson", geojson);
            await DirectoryHelper.WriteAllTextIfDataAsync($"{path}/features.json", language.Features);
            await DirectoryHelper.WriteAllTextIfDataAsync($"{path}/additionalDetails", language.AdditionalDetails);
        }

        public static string EnsureDataIsFeatureCollectiion(string json)
        {
            // First attempt, can be converted into FeatureCollection
            var collection = DataConverter.DeserializeGeoJson<FeatureCollection>(json);            
            if(collection != null && collection.Any())
            {
                return json;
            }

            var feature = DataConverter.DeserializeGeoJson<Feature>(json);
            if(feature != null && feature.Geometry != null)
            {
                var featureCollection = new FeatureCollection { feature };
                var geoJson = DataConverter.SerializeSpatialData(featureCollection);
                var result = EnsureDataIsFeatureCollectiion(geoJson);

                return result;
            }

            throw new InvalidOperationException($"The data can't be converted into FeatureCollection. The data: {json}");
        }

        public Language FolderToLanguage(string folder, ref int seedCounter, string LanguageFolder, string areaJsonSuffix)
        {
            var yearDirectory = Directory.GetDirectories(folder)[0];
            var files = Directory
                .GetFiles(yearDirectory)
                .Select(fileName => CreateColumn(fileName, yearDirectory))
                .ToDictionary(key => key.Name, value => value.Value);

            var years = yearDirectory.Replace(folder, "").TrimStart('\\');
            var (start, end) = ParseYearsFromTheFolderName(years);

            var lang = new Language()
            {
                Id = seedCounter++,
                Name = folder.Replace(LanguageFolder, "").Replace(areaJsonSuffix, "").TrimStart('\\'),
                YearStart = start,
                YearEnd = end,
                Area = ((FeatureCollection)files["area"]).ToGeometry(),
                Features = files.GetValueOrDefault<string>("features"),
                AdditionalDetails =files.GetValueOrDefault<string>("additionalDetails")
            };

            return lang;
        }

        public (string Name, object Value) CreateColumn(string filePath, string folderPart)
        {
            if (string.IsNullOrEmpty(filePath))
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
                "geojson" => DataConverter.DeserializeGeoJson<FeatureCollection>(data),
                _ => (object)data
            };

            return (columnName, columnValue);
        }

        public string YearsToFolderName(int yearStart, int yearEnd)
        {
            string yearStartString = yearStart < 0 ? $"{Tools.ConvertNegativeIntoPositive(yearStart)}BCE" : $"{yearStart}CE";
            string yearEndString = yearEnd < 0 ? $"{Tools.ConvertNegativeIntoPositive(yearEnd)}BCE" : $"{yearEnd}CE";

            return $"{yearStartString}-{yearEndString}";
        }

        public (int? Start, int? End) ParseYearsFromTheFolderName(string yearFolder)
        {
            if (string.IsNullOrEmpty(yearFolder))
            {
                throw new ArgumentException("The name of the year folder is null or empty.", nameof(yearFolder));
            }

            var yearArray = yearFolder.Split('-');

            if (yearArray.Length != 2)
            {
                throw new ArgumentException($"Cannot manipulate the string that is composed from the year folder '{yearFolder}'. Remove extra '-' characaters from it.", nameof(yearFolder));
            }

            return (Start: YearToInt(yearArray[0]), End: YearToInt(yearArray[1]));
        }

        /// <exception cref="FormatException">$"Unable to convert the value '{year}' into a integer number"</exception>
        public int YearToInt(string year)
        {
            if (string.IsNullOrEmpty(year))
            {
                throw new ArgumentException("the year is null or empty.", nameof(year));
            }

            if (year.EndsWith("BCE"))
            {
                year = "-" + year.Replace("BCE", "");
            }

            if (year.EndsWith("CE"))
            {
                year = year.Replace("CE", "");
            }

            if (int.TryParse(year, out var result))
            {
                return result;
            }

            throw new FormatException($"Unable to convert the value '{year}' into a integer number");
        }
    }
}
