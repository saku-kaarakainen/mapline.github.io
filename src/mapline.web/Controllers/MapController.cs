using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mapline.Web.Data;

namespace Mapline.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        private readonly ILogger<MapController> logger;
        private readonly IDbContextFactory<MaplineDbContext> contextFactory;

        public MapController(ILogger<MapController> logger, IDbContextFactory<MaplineDbContext> contextFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        [HttpGet]
        public async Task<IEnumerable<Language>> Get()
        {
            this.logger.LogDebug("GET api/map/");

            using var db = this.contextFactory.CreateDbContext();
            return await db.Languages.ToListAsync();
        }
    }
}
