using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignSystem.API.Entities
{
    public class Store
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        public string Suburb { get; set; }
        [MaxLength(255)]
        public string Address { get; set; }
        [MaxLength(10)]
        public string State { get; set; }

        [MaxLength(100)]
        public string Manager { get; set; }
        public string IpAddress { get; set; }
        public string SubMask { get; set; }
        public string Port { get; set; }
        public int? SignId { get; set; }

    }
}
