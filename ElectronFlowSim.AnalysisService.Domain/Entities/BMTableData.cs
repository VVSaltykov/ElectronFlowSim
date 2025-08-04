using ElectronFlowSim.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.AnalysisService.Domain.Entities
{
    public class BMTableData : Entity
    {
        public List<double> Z {  get; set; }
        public List<double> Bm { get; set; }
        public double bnorm { get; set; }
    }
}
