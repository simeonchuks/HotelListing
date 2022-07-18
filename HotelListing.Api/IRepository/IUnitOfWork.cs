using HotelListing.Api.Data.Model;
using HotelListing.Api.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Repository
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Country> CountriesRepository { get; }
        IGenericRepository<Hotel> HotelsRepository { get; }
        Task Save();
    }
}
