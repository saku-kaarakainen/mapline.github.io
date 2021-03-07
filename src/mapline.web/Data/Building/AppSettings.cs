using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data.Building
{
    /// <summary>
    /// Represents the configuration values 
    /// that area stored in the appSettings.json file in the <see cref="Mapline.Web"/> - project.
    /// </summary>
    public class AppSettings : IConfiguration
    {
        private readonly IConfiguration config;

        /// <summary>
        /// Initializes new instance of the <see cref="AppSettings"/> class.
        /// </summary>
        /// <param name="path"></param>
        public AppSettings(string path = "appSettings.json")
        {
            this.config = new ConfigurationBuilder()
                .AddJsonFile(path)
                .Build();
        }

        public string this[string key] 
        {
            get => this.config[key];
            set => this.config[key] = value;
        }

        public IEnumerable<IConfigurationSection> GetChildren() => this.config.GetChildren();       
        public IChangeToken GetReloadToken() => this.config.GetReloadToken();       
        public IConfigurationSection GetSection(string key) => this.config.GetSection(key);        
    }
}
