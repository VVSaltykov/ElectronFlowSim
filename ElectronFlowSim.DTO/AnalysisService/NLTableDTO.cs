using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.DTO.AnalysisService
{
    /// <summary>
    /// DTO для данных таблицы NL
    /// </summary>
    public class NLTableDTO
    {
        public List<int> N { get; set; }
        public List<int> L { get; set; }
    }
}
