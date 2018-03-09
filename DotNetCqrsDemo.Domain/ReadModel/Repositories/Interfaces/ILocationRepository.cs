using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces
{
    public interface ILocationRepository : IBaseRepository<LocationRM>
    {
        Task<IEnumerable<LocationRM>> GetAll();
        Task<IEnumerable<EmployeeRM>> GetEmployees(int locationID);
        Task<bool> HasEmployee(int locationID, int employeeID);
    }
}
