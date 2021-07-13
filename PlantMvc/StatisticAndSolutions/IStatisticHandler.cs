using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;
using Entities.Enums;

namespace StatisticAndSolutions
{
    interface IStatisticHandler
    {
        void ReturnList(SortedList<Sensor, int> DataSave);
        Data[] GetDataFromTermoSensor(DateTime start, DateTime end);
        Data[] GetDataFromWaterSensor(DateTime start, DateTime end);
        Data[] GetDataFromAirSensor(DateTime start, DateTime end);
        
    }
}
