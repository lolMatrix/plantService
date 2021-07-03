using DataBase.Interfaces;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase
{
    public class SensorRepository : IReposytory<Sensor>
    {
        private readonly SensorContext _sensorContext;

        public SensorRepository(SensorContext sensorContext)
        {
            _sensorContext = sensorContext;
        }

        public void Delete(Sensor model)
        {
            _sensorContext.Delete(model);
        }

        public Sensor[] GetAll()
        {
            return _sensorContext.Sensors.ToArray();
        }

        public Sensor GetById(int id)
        {
            return _sensorContext.Sensors.FirstOrDefault(x => x.Id == id);
        }

        public Sensor Save(Sensor model)
        {
            return _sensorContext.Save(model);
        }

        public Sensor Update(Sensor model)
        {
            var sensor = GetById(model.Id);

            if (sensor == null)
                return null;

            sensor.UpdateModel(model);
            _sensorContext.SaveChanges();

            return sensor;
        }
    }
}
