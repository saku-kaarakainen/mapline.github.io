using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mapline.Tests
{
    /// <summary>
    /// Configuration class for the unit tests.
    /// </summary>
    public class Config 
    {
        /// <summary>
        /// Creates the IConfiguration from the "appsettings.json".
        /// </summary>
        /// <returns></returns>
        public static IConfiguration GetAppJson()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }
    }
}
