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

    public class DataHelper : ILanguageHelper
    {
        // The data will be save into generated folder, so that it won't mix with the 'production' data
        // Now I just verify files manually before saving them into 'production' data.
        public string LanguagePathFolder { get; set; } = "./../../data/Generated-Language/";

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
            if (collection != null && collection.Any())
            {
                return json;
            }

            var feature = DataConverter.DeserializeGeoJson<Feature>(json);
            if (feature != null && feature.Geometry != null)
            {
                var featureCollection = new FeatureCollection { feature };
                var geoJson = DataConverter.SerializeSpatialData(featureCollection);
                var result = EnsureDataIsFeatureCollectiion(geoJson);

                return result;
            }

            throw new InvalidOperationException($"The data can't be converted into FeatureCollection. The data: {json}");
        }

        public string YearsToFolderName(int yearStart, int yearEnd)
        {
            string yearStartString = yearStart < 0 ? $"{Tools.ConvertNegativeIntoPositive(yearStart)}BCE" : $"{yearStart}CE";
            string yearEndString = yearEnd < 0 ? $"{Tools.ConvertNegativeIntoPositive(yearEnd)}BCE" : $"{yearEnd}CE";

            return $"{yearStartString}-{yearEndString}";
        }
    }
}
