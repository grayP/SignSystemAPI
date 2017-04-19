using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignSystem.API.Entities;

namespace SignSystem.API
{
    public static class CityInfoExtensions
    {
        public static void EnsureSeedDataForContext(this SignSystemInfoContext context)
        {
            Sign sign1 = new Sign()
            {
                Model = "64x64",
                Width = 64,
                Height = 64
            };
            Sign sign2 = new Sign()
            {
                Model = "320x96",
                Width = 320,
                Height = 96
            };

            if (!context.Sign.Any())
            {
                var signs = new List<Sign>()
                {
                    sign1,
                    sign2
                };
                context.Sign.AddRange(signs);
                context.SaveChanges();
            }
            if (!context.Store.Any())
            {
                var stores = new List<Store>()
                {
                    new Store
                    {
                        Name = "Kangaroo Point",
                        Manager = "brian",
                        IpAddress = "192.168.0.1",
                        SubMask = "255.255.255.255",
                        Port = "5200",
                        SignId = 1
                    },
                    new Store()
                    {
                        Name = "Cairns",
                        Manager = "Jodie",
                        IpAddress = "192.168.0.12",
                        SubMask = "255.255.255.255",
                        Port = "5200",
                        SignId = 2
                    }
                };
                context.Store.AddRange(stores);
                context.SaveChanges();
            }
        }
    }
}
