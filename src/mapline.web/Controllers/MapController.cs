using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Mapline.Web.Data;
using Mapline.Web.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using NetTopologySuite.Features;
using NetTopologySuite.Geometries;

namespace Mapline.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MapController : ControllerBase
    {
        private readonly ILogger<MapController> logger;
        private readonly IDbContextFactory<MaplineDbContext> contextFactory;
        private IConfiguration configuration;

        public MapController(ILogger<MapController> logger, IDbContextFactory<MaplineDbContext> contextFactory)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
        }

        [Route("languages")]
        [HttpGet]
        public FeatureCollection Languages()
        {
            this.logger.LogDebug("GET api/map/languages");

            using var db = this.contextFactory.CreateDbContext();

            return db.Languages.ToFeatureCollection();
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
