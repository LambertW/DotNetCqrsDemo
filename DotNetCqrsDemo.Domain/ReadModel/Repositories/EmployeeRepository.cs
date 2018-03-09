
using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.ReadModel.Repositories
{
    public class EmployeeRepository : BaseRepository, IEmployeeRepository
    {
        public EmployeeRepository(IConnectionMultiplexer redisConnection) : base(redisConnection, "employee")
        {
        }

        public async Task<IEnumerable<EmployeeRM>> GetAll()
        {
            return await Get<List<EmployeeRM>>("all");
        }

        public async Task<EmployeeRM> GetByID(int employeeID)
        {
            return await Get<EmployeeRM>(employeeID);
        }

        public List<EmployeeRM> GetMultiple(List<int> employeeIDs)
        {
            return GetMultiple<EmployeeRM>(employeeIDs);
        }

        public async Task Save(EmployeeRM employee)
        {
            await Save(employee.EmployeeID, employee);
            await MergeIntoAllCollection(employee);
        }

        private async Task MergeIntoAllCollection(EmployeeRM employee)
        {
            List<EmployeeRM> allEmployees = new List<EmployeeRM>();
            if (Exists("all"))
            {
                allEmployees = await Get<List<EmployeeRM>>("all");
            }

            if (allEmployees.Any(x => x.EmployeeID == employee.EmployeeID))
            {
                allEmployees.Remove(allEmployees.First(x => x.EmployeeID == employee.EmployeeID));
            }

            allEmployees.Add(employee);

            await Save("all", allEmployees);
        }
    }
}
