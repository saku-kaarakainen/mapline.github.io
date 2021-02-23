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
        private const string areaJsonSuffix = "\\area.geojson";
        private readonly string languageFolder;
        private readonly string[] names;
        private readonly DataHelper helper = DataHelper.Instance;
        private readonly List<LanguageFolder> data;

        public LanguagesBuilder(string languageFolder)
        {
            if(string.IsNullOrEmpty(languageFolder))
            {
                throw new ArgumentException("the name of the language folder is null or empty.", nameof(languageFolder));
            }

            this.data = new List<LanguageFolder>();
            this.languageFolder = languageFolder;
            this.names = Directory.GetDirectories(this.languageFolder);

            foreach (var nameDirectory in this.names)
            foreach (var yearDirectory in Directory.GetDirectories(nameDirectory))
            {
                var years = yearDirectory.Replace(this.languageFolder, "").TrimStart('\\');
                var (start, end) = helper.ParseYearsFromTheFolderName(years);
                this.data.Add(new LanguageFolder
                {
                    Language = new Language
                    {
                        Name = nameDirectory,
                        YearStart = start,
                        YearEnd = end
                    }
                });
            }
        }

        public void PopulateFromFolder()
        {
            foreach(var language in this.data)
            {

            }
        }

        class LanguageFolder
        {
            public string Folder { get; set; }
            public Language Language { get; set; }
        }
    }
}
