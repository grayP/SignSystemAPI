using System.Collections.Generic;
using SignSystem.API.Entities;

namespace SignSystem.API.Services.Stores
{
    public interface IStoreRepository
    {
        bool StoreExists(int storeId);
        bool StoreExists(string storeName);
        IEnumerable<Store> GetStores();
        Store GetStore(int storeId);

        void Add(Store store);
        void DeleteStore(Store store);
        bool Save();
    }
}
