using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdessoRideShare.Domain.Models;

namespace AdessoRideShare.Infrastructure.Data.Mappings
{
    public class RidePlanMap : IEntityTypeConfiguration<RidePlan>
    {
        public void Configure(EntityTypeBuilder<RidePlan> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.CustomerId)
                .IsRequired();

            builder.Property(c => c.FromCityId)
                .IsRequired();

            builder.Property(c => c.ToCityId)
                .IsRequired();

            builder.Property(c => c.Date)
                .IsRequired();

            builder.Property(c => c.Description)
                .HasMaxLength(150)
                .IsRequired();

            builder.Property(c => c.SeatCount)
                .IsRequired();

            builder.Property(c => c.IsPublished)
                .IsRequired();
        }
    }
}
