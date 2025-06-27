using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.DTO.API
{
    public class MagneticFieldsDTO
    {
        [Required]
        public double StartPoint { get; set; }
        [Required]
        public double EndPoint { get; set; }
        [Required]
        [Range(0.1, 5)]
        public double Step { get; set; }
        [Required]
        public IFormFile File { get; set; }
    }
}
