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
        public static List<HistoricBase> ListHistorics { get; } = new List<HistoricBase>();

        public static void AddListHistorics<T>(Historic<T> historic) where T : Models.Entity
        {
            ListHistorics.Add(historic);
        }

        public static HistoricBase GetHistoricBase(string name) => ListHistorics.FirstOrDefault(h => h.Name.Equals(name));
    }
}
