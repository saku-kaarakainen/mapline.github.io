using Mapline.Web.Utils;
using Microsoft.Extensions.Configuration;
using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data.Building
{
    // data
    //  -> Language
    //      -> {language.Name}
    //          -> { language.Year}
    //              -> area.geojson
    //              -> filters.txt (filters of the language)
    //              -> additionalDetails.json
    //              -> features.json
    //  -> filters.json (Filter table)
    public class LanguagesBuilder
    {
        private readonly IDirectory directory;
        private readonly string languageFolder;
        private readonly string areaJsonSuffix;
        private readonly IEnumerable<string> names;
        private readonly List<LanguageFolder> data;

        public int Counter { get; set; } = 0;

        public IEnumerable<Language> Languages => data.Select(d => d.Language);

        public static IDataBuilder CreateDataBuilder()
        {
            return CreateDataBuilder(new InMemoryConfiguration
            {
                Data = new Dictionary<string, string>
                {
                    { "languageFolder", "\\area.geojson" },
                    { "areaJsonSuffix", "..\\..\\data\\Language" },
                    { "counter", "1" },
                }
            });
        }

        public static IDataBuilder CreateDataBuilder(IConfiguration config)
        {
            if (config == null)
            {
                throw new ArgumentNullException(nameof(config));
            }

            var languageFolder = config.GetValue<string>("languageFolder");
            var areaJsonSuffix = config.GetValue<string>("areaJsonSuffix");
            var counter = config.GetValue<int>("counter");
            var helper = new DirectoryHelper();
            var builder = new LanguagesBuilder(helper, languageFolder, areaJsonSuffix) { Counter = counter };
            builder.CreateData();

            return new DataBuilder(builder.Languages);
        }

        public LanguagesBuilder(IDirectory directory, string languageFolder, string areaJsonSuffix)
        {
            if (string.IsNullOrEmpty(languageFolder))
            {
                throw new ArgumentException("the name of the language folder is null or empty.", nameof(languageFolder));
            }

            this.data = new List<LanguageFolder>();
            this.directory = directory ?? throw new ArgumentNullException(nameof(directory));
            this.languageFolder = languageFolder;
            this.areaJsonSuffix = areaJsonSuffix;
            this.names = this.directory.GetDirectories(this.languageFolder);
        }

        public void CreateData()
        {
            foreach (string nameDirectory in this.names)
            {
                foreach (string yearDirectory in this.directory.GetDirectories(nameDirectory))
                {
                    var name = nameDirectory.Replace(this.languageFolder, "").Replace(this.areaJsonSuffix, "").TrimStart('\\');
                    var years = yearDirectory.Replace(nameDirectory, "").TrimStart('\\');
                    var (start, end) = ParseYearsFromTheFolderName(years);

                    var item = new LanguageFolder
                    {
                        YearDirectory = years,
                        Files = this.directory
                            .GetFiles(yearDirectory)
                            .Select(fileName => CreateColumn(fileName, yearDirectory))
                            .ToDictionary(key => key.Name, value => value.Value),
                        Language = new Language
                        {
                            Id = Counter++,
                            Name = name,
                            YearStart = start,
                            YearEnd = end
                        }
                    };

                    if (!item.Files.ContainsKey("area"))
                    {
                        throw new InvalidOperationException($"The folder '{name}' does not have file 'area.json'.");
                    }

                    item.Language.Area = ((FeatureCollection)item.Files["area"]).ToGeometry();
                    item.Language.Features = item.Files.GetValueOrDefault<string>("features");
                    item.Language.AdditionalDetails = item.Files.GetValueOrDefault<string>("additionalDetails");
                    item.SetFilters("filters");

                    this.data.Add(item);
                }
            }
        }

        private static (string Name, object Value) CreateColumn(string filePath, string folderPart)
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
            var columnType = filename[(index + 1)..];

            using var reader = new StreamReader(filePath);
            string data = reader.ReadToEnd();

            var columnValue = columnType switch
            {
                "geojson" => DataConverter.DeserializeGeoJson<FeatureCollection>(data),
                _ => (object)data
            };

            return (columnName, columnValue);
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

        class LanguageFolder
        {
            public Dictionary<string, object> Files { get; set; }
            public string YearDirectory { get; set; }
            public Language Language { get; set; }

            public void SetFilters(string filterKey)
            {
                Language.AddFilters(filters: GetFilters(filterKey));
            }

            private IEnumerable<Filter> GetFilters(string dictionaryKey)
            {
                string separator = Environment.NewLine;

                return Files
                    .GetValueOrDefault<string>(dictionaryKey)
                    ?.Split(new string[] { separator }, StringSplitOptions.RemoveEmptyEntries)
                    ?.Select((string item, int index) =>
                    {
                        if (long.TryParse(item, out var id))
                        {
                            return new Filter(id);
                        }

                        throw new FormatException($"Unable to cast the value '{item}' to long at the index of '{index}' in the file '{index}'");
                    });
            }
        }
    }
}
