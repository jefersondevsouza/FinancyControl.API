using FinancyControl.API.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancyControl.API.Persistence.Contexts.Configurations
{
    public class TipoDespesaConfiguration : IEntityTypeConfiguration<TipoDespesa>
    {
        public void Configure(EntityTypeBuilder<TipoDespesa> builder)
        {
            builder.ToTable("TiposDespesas");
            builder.HasKey(p => p.Id);
            builder.Property(p => p.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(p => p.Name).IsRequired().HasMaxLength(30);
            builder.HasMany(p => p.Despesas).WithOne(p => p.TipoDespesa).HasForeignKey(p => p.TipoDespesaId);
        }
    }
}
