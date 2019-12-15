using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using AdessoRideShare.Domain.Models;

namespace AdessoRideShare.Infrastructure.Data.Mappings
{
    public class BookingMap : IEntityTypeConfiguration<Booking>
    {
        public void Configure(EntityTypeBuilder<Booking> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.CustomerId)
                .IsRequired();

            builder.Property(c => c.RidePlanId)
                .IsRequired();

            builder.Property(c => c.BookedSeatCount)
                .IsRequired();
        }
    }
}
