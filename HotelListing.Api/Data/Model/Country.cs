using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Data.Model
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShortName { get; set; }

        public virtual IList<Hotel> Hotels { get; set; }
        //NOTE: This property does not have to be migrated to the data base, it simply means, Give me a country with a particular id and return the list of Hotels.
    }
}
