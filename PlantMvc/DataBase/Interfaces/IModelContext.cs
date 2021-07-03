using Entities;
using System.Collections.Generic;

namespace DataBase
{
    interface IModelContext<T> where T : Model
    {
        T Save(T model);

        void Delete(T model);
    }
}