using NetTopologySuite.Geometries;

namespace mapline.Data
{
    /// <remarks>Note: This is being used as EF Class</remarks>
    public partial class Language
    {        
        public long Id { get; set; }

        public string StringIdentifier { get; set; }

        public Geometry Area { get; set; }

        public int? YearStart { get; set; }

        public int? YearEnd { get; set; }

        public int? YearCurrent { get; set; }

        public string Features { get; set; }

        public string AdditionalDetails { get; set; }
    } 
}

namespace mapline.Data.Magic
{
   using System;
   using System.Linq;
   using NetTopologySuite.Features;

   /// <summary>
   /// Represents the magic version of <see cref="Data.Language"/>.
   /// </summary>
   /// <remarks>
   /// Magic classes are used to deserialize classes, which can't be deserialized as is.
   /// </remarks>
   public partial class Language
   {
       public string StringIdentifier { get; set; }

       public int? YearStart { get; set; }

       public int? YearEnd { get; set; }

       public int? YearCurrent { get; set; }

       public object Features { get; set; }

       public object AdditionalDetails { get; set; }

       public static explicit operator Data.Language(Magic.Language magicClass) 
            => magicClass == default ? default : new Data.Language
       {
           Id = default,
           StringIdentifier = magicClass.StringIdentifier,
           YearStart = magicClass.YearStart,
           YearEnd = magicClass.YearEnd,
           YearCurrent = magicClass.YearCurrent,
           Area = null,
           Features = magicClass.Features.ToString(),
           AdditionalDetails = magicClass.AdditionalDetails.ToString()
       };        
   }
}
