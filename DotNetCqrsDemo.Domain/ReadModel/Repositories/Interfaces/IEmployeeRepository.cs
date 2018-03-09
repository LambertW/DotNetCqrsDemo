using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces
{
    public interface IEmployeeRepository : IBaseRepository<EmployeeRM>
    {
        Task<IEnumerable<EmployeeRM>> GetAll();
    }
}
