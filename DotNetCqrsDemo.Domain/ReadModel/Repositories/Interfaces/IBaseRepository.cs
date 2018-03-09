using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> GetByID(int id);
        List<T> GetMultiple(List<int> ids);
        bool Exists(int id);
        Task Save(T item);
    }
}
