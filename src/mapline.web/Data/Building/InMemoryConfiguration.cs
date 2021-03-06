using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mapline.Web.Data.Building
{
    public class InMemoryConfiguration : IConfiguration
    {
        public Dictionary<string, string> Data { get; set; }
        public string this[string key]
        {
            get => this.Data[key];
            set
            {
                if (this.Data.ContainsKey(key))
                {
                    this.Data[key] = value;
                }
                else
                {
                    this.Data.Add(key, value);
                }
            }
        }

        public IEnumerable<IConfigurationSection> GetChildren()
        {
            throw new NotImplementedException();
        }
        
        public IChangeToken GetReloadToken()
        {
            throw new NotImplementedException();
        }

        public IConfigurationSection GetSection(string key)
        {
            var s = this as IConfigurationSection;

            return s;
        }
    }
}
