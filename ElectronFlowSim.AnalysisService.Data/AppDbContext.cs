using ElectronFlowSim.AnalysisService.Data.EntityConfigurations;
using ElectronFlowSim.AnalysisService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.AnalysisService.Data
{
    /// <summary>
    /// БД контекст
    /// </summary>
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options) { }


        public DbSet<InputData> InputDatas { get; set; }
        public DbSet<NLTableData> NLTableDatas { get; set; }
        public DbSet<NZRUTableData> NZRUTableDatas { get; set; }
        public DbSet<BMTableData> BMTableDatas { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new InputDataConfiguration());
        }
    }
}
