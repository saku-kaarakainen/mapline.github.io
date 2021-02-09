using Mapline.Web.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mapline.Tests
{
    public class TestDbContext : MaplineDbContext
    {
        public TestDbContext(DbContextOptions<MaplineDbContext> options)
             : base(options)
        {
            
        }
    }
}
