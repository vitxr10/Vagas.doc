using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using VagasDoc.Models;

namespace VagasDoc.Data.Map
{
    public class VagaMap : IEntityTypeConfiguration<VagaModel>
    {
        public void Configure(EntityTypeBuilder<VagaModel> builder)
        {
            builder.HasKey(x => x.Id);
            builder.HasOne(x => x.Usuario);
        }
    }
}
