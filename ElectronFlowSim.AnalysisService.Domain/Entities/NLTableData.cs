using ElectronFlowSim.Domain.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectronFlowSim.AnalysisService.Domain.Entities
{
    /// <summary>
    /// Данные для таблицы NL
    /// </summary>
    public class NLTableData : Entity
    {
        public List<int> N { get; set; }
        public List<int> L { get; set; }
    }
}
