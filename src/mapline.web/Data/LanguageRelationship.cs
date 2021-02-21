using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class LanguageRelationship
    {
        public RelationshipType Type { get; set; }

        public long ParentId { get; set; }
        public Language Parent { get; set; }

        public long ChildID { get; set; }
        public Language Child { get; set; }
    }

    public enum RelationshipType
    {
        Genetic,
        Coincidence,
        LanguageContact,
        Unknown
    }
}
