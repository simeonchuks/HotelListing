using HotelListing.Api.Data.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Configurations.DataSeed
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasData(
                  new Country
                  {
                      Id = 1,
                      Name = "Nigeria",
                      ShortName = "NG"
                  },
                  new Country
                  {
                      Id = 2,
                      Name = "Jamiaca",
                      ShortName = "JM"
                  },
                  new Country
                  {
                      Id = 3,
                      Name = "Brazil",
                      ShortName = "BR"
                  }
              );
        }
    }
}
