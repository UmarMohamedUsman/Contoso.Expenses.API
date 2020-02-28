using Contoso.Expenses.API.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Configuration;
using Microsoft.Extensions.Options;

namespace Contoso.Expenses.API.Database
{
    public class DatabaseContext : DbContext
    {
        private readonly IOptions<ConfigValues> _config;
        public DatabaseContext(IOptions<ConfigValues> config)
        {
            _config = config;
        }

        public DbSet<CostCenter> costCenters { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_config.Value.SQLConnectionString);
        }
    }
}
