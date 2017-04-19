using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SignSystem.API.Entities;

namespace SignSystem.API.Models.Stores
{
    public class StoreDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Suburb { get; set; }
        public string Address { get; set; }
        public string State { get; set; }
        public string Manager { get; set; }
        public string IpAddress { get; set; }
        public string SubMask { get; set; }
        public int Port { get; set; }
        public int? SignId { get; set; }
        public Sign sign => new Sign(){Id=SignId ?? 0};
    }
}
