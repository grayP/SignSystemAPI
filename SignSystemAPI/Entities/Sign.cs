using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;

namespace SignSystem.API.Entities
{
    public class Sign
    {
        [Key]
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Model { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
