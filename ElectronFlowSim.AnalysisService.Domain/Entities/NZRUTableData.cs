using ElectronFlowSim.AnalysisService.Domain.Enum;
using ElectronFlowSim.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.AnalysisService.Domain.Entities
{
    public class NZRUTableData : Entity
    {
        public List<int> N { get; set; }
        public List<double> R { get; set; }
        public List<double> Z { get; set; }
        public double U { get; set; }
        public WorkpieceType WorkpieceType { get; set; }
    }
}
