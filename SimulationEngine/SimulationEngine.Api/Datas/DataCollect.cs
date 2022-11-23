using SimulationEngine.Api.Base;
using SimulationEngine.Api.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationEngine.Api.Datas
{
    public static class DataCollect
    {
        public static List<HistoricBase> HistoricList { get; } = new List<HistoricBase>();

        public static void AddListHistorics<T>(Historic<T> historic) where T : Entity
        {
            HistoricList.Add(historic);
        }

        public static HistoricBase GetHistoricBase(string name) => HistoricList.FirstOrDefault(h => h.Name.Equals(name));
    }
}
