using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Interfaces
{
    interface IReposytory<T> : IModelContext<T>
        where T : Model
    {
        T[] GetAll();

        T GetById(int id);

        T Update(T model);
    }
}
