using ElectronFlowSim.AnalysisService.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.AnalysisService.Data.EntityConfigurations
{
    /// <summary>
    /// Конфигурация БД для входных данных 
    /// </summary>
    public class InputDataConfiguration : IEntityTypeConfiguration<InputData>
    {
        public void Configure(EntityTypeBuilder<InputData> builder)
        {
            builder.HasOne<NLTableData>(a => a.NLTableData)
            .WithMany()
            .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany<NZRUTableData>(a => a.NZRUTableDatas)
            .WithMany();

            builder.HasOne(a => a.BMTableData)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
