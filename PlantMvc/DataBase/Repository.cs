using DataBase.Interfaces;
using Entities;
using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

namespace DataBase
{
    /// <summary>
    /// Класс для работы с базой данных.
    /// </summary>
    /// <typeparam name="T">Модель, описывающие данные.</typeparam>
    public class Repository<T> : IRepository<T> where T : Model
    {
        private readonly Context _context;

        private DbSet<T> _set;

        public Repository(Context context)
        {
            _context = context;
            _set = _context.Set<T>();
        }

        /// <summary>
        /// Удаляет сущность из бд
        /// </summary>
        /// <param name="model">Данные, которые нужно удалить</param>
        public void Delete(T model)
        {
            _set.Remove(model);
            _context.SaveChanges();
        }

        public T[] Select(Func<T, bool> expresion)
        {
            return _set.Where(expresion).ToArray();
        }

        /// <summary>
        /// Получить все записи
        /// </summary>
        /// <returns>Все записи в бд типа Т</returns>
        public T[] GetAll()
        {
            return _set.AsNoTracking().ToArray();
        }

        /// <summary>
        /// Получить запиь по id 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Данные с нужным id</returns>
        public T GetById(int id)
        {
            return _set.Find(id);
        }

        /// <summary>
        /// Сохраняет данные в базе данных
        /// </summary>
        /// <param name="model">Данные</param>
        /// <returns>Сохраненную сущность с id</returns>
        public T Save(T model)
        {
            _set.Add(model);
            _context.SaveChanges();
            return model;
        }

        /// <summary>
        /// Обновляет данные в бд
        /// </summary>
        /// <param name="model">Данные</param>
        /// <returns>Обновленные данные</returns>
        public T Update(T model)
        {
            _context.Entry(model).State = EntityState.Modified;

            _context.SaveChanges();

            return model;
        }
    }
}
