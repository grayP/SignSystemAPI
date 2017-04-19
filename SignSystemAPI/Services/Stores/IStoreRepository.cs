using System.Collections.Generic;
using SignSystem.API.Entities;

namespace SignSystem.API.Services.Stores
{
    public interface IStoreRepository
    {
        bool StoreExists(int storeId);
        IEnumerable<Store> GetStores();
        Store GetStore(int storeId);
        void DeleteStore(Store store);
        bool Save();
    }
}
