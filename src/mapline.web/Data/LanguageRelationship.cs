using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class LanguageRelationship
    {
        public long Id { get; set; }

        public RelationshipType Type { get; set; }

        public long ParentId { get; set; }
        public Language Parent { get; set; }

        public long ChildId { get; set; }
        public Language Child { get; set; }
    }
}
