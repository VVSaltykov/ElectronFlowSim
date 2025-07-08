using ElectronFlowSim.DTO.AnalysisService.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.DTO.AnalysisService
{
    public class NZRUTableDTO
    {
        public WorkpieceType WorkpieceType { get; set; }
        public List<int> N { get; set; }
        public List<double> Z { get; set; }
        public List<double> R { get; set; }
        public double U { get; set; }
    } 
}
