using DataBase;
using Entities.Models;
using System;
using System.Linq;

namespace StatisticAndSolutions
{
    public class StatisticHandler
    {
        private readonly Repository<Data> _repository;

        public StatisticHandler(Repository<Data> repository)
        {
            _repository = repository;
        }

        public Data[] GetDataFromSensorForPeriodById(DateTime first, DateTime second, int id)
        {
            var data = _repository.Select(x => x.SensorId == id && x.TimeMeasurement < second && x.TimeMeasurement > first);
            return data.ToArray();
        }

    }
}
