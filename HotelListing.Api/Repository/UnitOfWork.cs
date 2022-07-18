using HotelListing.Api.Data;
using HotelListing.Api.Data.Model;
using HotelListing.Api.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HotelListing.Api.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        private IGenericRepository<Country> _countriesRepository;
        private IGenericRepository<Hotel> _hotelsRepository; 

        public UnitOfWork(AppDbContext context)
        {
            _context = context;
        }

        public IGenericRepository<Country> CountriesRepository => _countriesRepository ??= new GenericRepository<Country>(_context); //If the private country repository is null, create a new instance of country repository

        public IGenericRepository<Hotel> HotelsRepository => _hotelsRepository ??= new GenericRepository<Hotel>(_context);

        public async Task Save()
        {
            await _context.SaveChangesAsync();
        }

        public void Dispose()
        {
            _context.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}
