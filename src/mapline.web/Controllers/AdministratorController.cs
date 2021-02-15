using Mapline.Web.Data;
using Mapline.Web.Models;
using Mapline.Web.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc; 
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AdministratorController : ControllerBase
    {
        private readonly ILogger<AdministratorController> logger;
        private readonly IDbContextFactory<MaplineDbContext> contextFactory;
        private readonly ILanguageHelper languageHelper;

        public AdministratorController(ILogger<AdministratorController> logger, 
            IDbContextFactory<MaplineDbContext> contextFactory, 
            ILanguageHelper languageHelper)
        {
            this.logger = logger ?? throw new ArgumentNullException(nameof(logger));
            this.contextFactory = contextFactory ?? throw new ArgumentNullException(nameof(contextFactory));
            this.languageHelper = languageHelper ?? throw new ArgumentNullException(nameof(languageHelper));
        }

        [Route("save/db")]
        [HttpPost]
        public async Task<ActionResult> SaveToDatabase(SaveLanguageModel language)
        {
            try
            {
                this.logger.LogDebug("POST api/administrator/save/db");
                using var db = this.contextFactory.CreateDbContext();
                await db.Languages.AddAsync(language);

                await db.SaveChangesAsync();

                return Ok();
            }
            catch(Exception exception)
            {
                this.logger.LogError($"PUT api/administrator/save/database - Error - The exception: {exception}");
                return StatusCode(StatusCodes.Status500InternalServerError, exception);
            }
        }

        [Route("save/file")]
        [HttpPost]
        public async Task<ActionResult> SaveToFile(SaveLanguageModel language)
        {
            try
            {
                this.logger.LogDebug("POST api/administrator/save/file");

                await this.languageHelper.SaveLanguage(language);

                return Ok();
            }
            catch(Exception exception)
            {
                this.logger.LogError($"PUT api/administrator/save/file - Error - The exception: {exception}");
                return StatusCode(StatusCodes.Status500InternalServerError, exception);
            }
        }
    }
}
