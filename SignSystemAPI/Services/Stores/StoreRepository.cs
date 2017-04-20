using System;
using System.Collections.Generic;
using System.Linq;
using SignSystem.API.Entities;

namespace SignSystem.API.Services.Stores
{
    public class StoreRepository : IStoreRepository
    {
        private SignSystemInfoContext _context;
        public StoreRepository(SignSystemInfoContext context)
        {
            _context = context;
        }


        public bool StoreExists(int storeId)
        {
            return _context.Store.Any(c => c.Id == storeId);
        }

        public IEnumerable<Store> GetStores()
        {
            return _context.Store.OrderBy(c => c.Name).ToList();
        }

        public Store GetStore(int storeId)
        {
            return _context.Store.FirstOrDefault(c => c.Id == storeId);
        }

        public void DeleteStore(Store store)
        {
            _context.Store.Remove(store);
        }

        public bool Save()
        {
            return (_context.SaveChanges() >= 0);
        }

        public void Add(Store store)
        {
            _context.Add(store);
        
        }

        public bool StoreExists(string storeName)
        {
            return _context.Store.Any(c => c.Name == storeName);
        }
    }
}
