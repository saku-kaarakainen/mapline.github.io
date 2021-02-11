using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Mapline.Web.Data;
using Mapline.Web.Models;

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

        [Route("languages")]
        [HttpGet]
        public async Task<IEnumerable<Language>> Languages()
        {
            this.logger.LogDebug("GET api/map/languages");

            using var db = this.contextFactory.CreateDbContext();
            return await db.Languages.ToListAsync();
        }

        [Route("filters")]
        [HttpGet]
        public async Task<IEnumerable<Filter>> Filters()
        {
            // TODO: The real implement
            await Task.Delay(1);
            this.logger.LogDebug("GET api/map/filters/");

            return new List<Filter>            
            {
                new Filter("uralic", true),
                new Filter("indo-european", false)
            };
        }
    }
}
