using HotelListing.Api.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace HotelListing.Api.Configurations.DataSeed
{
    public class HotelConfiguration : IEntityTypeConfiguration<Hotel>
    {
        
public void Configure(EntityTypeBuilder<Hotel> builder)
        {
            builder.HasData(
               new Hotel
               {
                   Id = 1,
                   Name = "Eko Hotel and Suit",
                   Address = "Victoria Island",
                   CountryId = 1,
                   Rating = 4.5
               },
               new Hotel
               {
                   Id = 2,
                   Name = "Girona Hotels",
                   Address = "Bash Island",
                   CountryId = 2,
                   Rating = 5.0
               },
               new Hotel
               {
                   Id = 3,
                   Name = "Bras Hotel and Suit",
                   Address = "Rio",
                   CountryId = 3,
                   Rating = 4.0
               }
           );
        }
    }
}