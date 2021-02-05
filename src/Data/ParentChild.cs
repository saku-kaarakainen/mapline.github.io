using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace mapline.Data
{
    public partial class LanguageParentChild
    {
        public int Id { get; set; }
        public Language Parent { get; set; }
        public Language Child { get; set; }
    }
}