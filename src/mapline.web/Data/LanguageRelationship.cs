using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data
{
    public class LanguageRelationship
    {
        public long Id { get; set; }

        public RelationshipType Type { get; set; }

        public long? ParentId { get; set; }
        [ForeignKey("ParentId")]
        public virtual Language Parent { get; set; }

        public long? ChildId { get; set; }
        [ForeignKey("ChildId")]
        public virtual Language Child { get; set; }
    }
}
