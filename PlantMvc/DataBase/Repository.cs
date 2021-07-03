using DataBase.Interfaces;
using Entities;
using System;
using System.Linq;

namespace DataBase
{
    /// <summary>
    /// Класс для работы с базой данных.
    /// </summary>
    /// <typeparam name="T">Модель, описывающие данные.</typeparam>
    public class Repository<T> : IRepository<T> where T : Model
    {
        private readonly Context<T> _context;

        public Repository(Context<T> context)
        {
            _context = context;
        }

        /// <summary>
        /// Удаляет сущность из бд
        /// </summary>
        /// <param name="model">Данные, которые нужно удалить</param>
        public void Delete(T model)
        {
            _context.Delete(model);
        }

        public T[] Select(Func<T, bool> expresion)
        {
            return _context.Sensors.Where(expresion).ToArray();
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Все записи в бд типа Т</returns>
        public T[] GetAll()
        {
            return _context.Sensors.ToArray();
        }

        /// <summary>
        /// Получить запиь по id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Данные с нужным id</returns>
        public T GetById(int id)
        {
            return _context.Sensors.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// Сохраняет данные в базе данных
        /// </summary>
        /// <param name="model">Данные</param>
        /// <returns>Сохраненную сущность с id</returns>
        public T Save(T model)
        {
            return _context.Save(model);
        }

        /// <summary>
        /// Обновляет данные в бд
        /// </summary>
        /// <param name="model">Данные</param>
        /// <returns>Обновленные данные</returns>
        public T Update(T model)
        {
            var sensor = GetById(model.Id);

            if (sensor == null)
                return null;

            sensor.UpdateModel(model);
            _context.SaveChanges();

            return sensor;
        }
    }
}
