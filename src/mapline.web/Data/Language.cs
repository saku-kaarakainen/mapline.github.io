﻿using NetTopologySuite.Geometries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Newtonsoft.Json.Converters;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using NetTopologySuite.IO.Converters;
using System.ComponentModel.DataAnnotations.Schema;
using NetTopologySuite.Features;

namespace Mapline.Web.Data
{
    /// <summary>
    /// Database reprentation of Language.
    /// </summary>
    public class Language : IFeatureable
    {
        public Language()
        {
            this.ParentRelationships = new HashSet<LanguageRelationship>();
            this.ChildRelationships = new HashSet<LanguageRelationship>();
            this.LanguageFilters = new HashSet<LanguageFilter>();
        }

        public long Id { get; set; }
        public string Name { get; set; }

        [JsonProperty(PropertyName = "geometry", ItemConverterType = typeof(GeometryConverter))]
        public virtual Geometry Area { get; set; }

        public virtual ICollection<LanguageRelationship> ParentRelationships { get; set; }
        public virtual ICollection<LanguageRelationship> ChildRelationships { get; set; }

        public virtual ICollection<LanguageFilter> LanguageFilters { get; set; }

        public void AddFilters(params Filter[] filters) => AddFilters(filters.AsEnumerable());
        public void AddFilters(IEnumerable<Filter> filters)
        {
            if(filters == null || !filters.Any())
            {
                return;
            }

            foreach (var filter in filters)
            {
                LanguageFilters.Add(new LanguageFilter(this, filter));
            }
        }
        
        public int? YearStart { get; set; }
        public int? YearEnd { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }

        // 2021-02-20: You would be able to create this as more dynamic, using System.Reflection
        // However I don't recommend it. It would require, not just code redesign, but it would drop reliability.
        // It wouldn't bring any other benenfit, than just you don't need to write yourself ToFeature method.
        // You would need to specify anyway which properties to ne ignored and which not. This is the safest, and probably the cleanest option
        public Feature ToFeature() => new(Area, new AttributesTable
        {
            { "name", Name },
            { "yearStart", YearStart },
            { "yearEnd", YearEnd },
            { "languageProperties", Features },
            { "additionalDetails", AdditionalDetails }
        });
    }
}

