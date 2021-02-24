using Mapline.Web.Utils;
using NetTopologySuite.Features;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
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
        private readonly string languageFolder;
        private readonly string areaJsonSuffix;
        private readonly string[] names;
        // TODO: Get rid of helper
        private readonly DataHelper helper = DataHelper.Instance;
        private readonly List<LanguageFolder> data;
        
        public int LastIndex { get; private set; }

        public IEnumerable<Language> Languages => data.Select(d => d.Language);

        public LanguagesBuilder(string languageFolder, string areaJsonSuffix, int seedIndex)
        {
            if (string.IsNullOrEmpty(languageFolder))
            {
                throw new ArgumentException("the name of the language folder is null or empty.", nameof(languageFolder));
            }

            this.data = new List<LanguageFolder>();
            this.languageFolder = languageFolder;
            this.areaJsonSuffix = areaJsonSuffix;
            this.names = Directory.GetDirectories(this.languageFolder);

            LastIndex = seedIndex;

            CreateData();
        }

        private void CreateData()
        {
            foreach (string nameDirectory in this.names)
            {
                foreach (string yearDirectory in Directory.GetDirectories(nameDirectory))
                {
                    var name = nameDirectory.Replace(this.languageFolder, "").Replace(this.areaJsonSuffix, "").TrimStart('\\');
                    var years = yearDirectory.Replace(nameDirectory, "").TrimStart('\\');
                    var (start, end) = helper.ParseYearsFromTheFolderName(years);

                    var item = new LanguageFolder
                    {
                        YearDirectory = years,
                        Files = Directory
                            .GetFiles(yearDirectory)
                            .Select(fileName => helper.CreateColumn(fileName, yearDirectory))
                            .ToDictionary(key => key.Name, value => value.Value),
                        Language = new Language
                        {
                            Id = LastIndex++,
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
