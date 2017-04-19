using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SignSystem.API.Models.Signs
{
    public class SignForUpdateDto
    {
        [Required(ErrorMessage = "You should provide a name for the sign.")]
        [MaxLength(100)]
        public string Model { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }
}
