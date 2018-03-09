using DotNetCqrsDemo.Domain.ReadModel.Repositories.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetCqrsDemo.Domain.ReadModel.Repositories
{
    public class LocationRepository : BaseRepository, ILocationRepository
    {
        public LocationRepository(IConnectionMultiplexer redisConnection) : base(redisConnection, "location")
        {
        }
        public async Task<IEnumerable<LocationRM>> GetAll()
        {
            return await Get<List<LocationRM>>("all");
        }

        public async Task<LocationRM> GetByID(int employeeID)
        {
            return await Get<LocationRM>(employeeID);
        }

        public async Task<IEnumerable<EmployeeRM>> GetEmployees(int locationID)
        {
            return await Get<List<EmployeeRM>>(locationID.ToString() + ":employees");
        }

        public List<LocationRM> GetMultiple(List<int> employeeIDs)
        {
            return GetMultiple<LocationRM>(employeeIDs);
        }

        public async Task<bool> HasEmployee(int locationID, int employeeID)
        {
            var location = await Get<LocationRM>(locationID);

            return location.Employees.Contains(employeeID);
        }

        public async Task Save(LocationRM employee)
        {
            await Save(employee.LocationID, employee);
            await MergeIntoAllCollection(employee);
        }

        private async Task MergeIntoAllCollection(LocationRM employee)
        {
            List<LocationRM> allEmployees = new List<LocationRM>();
            if (Exists("all"))
            {
                allEmployees = await Get<List<LocationRM>>("all");
            }

            if (allEmployees.Any(x => x.LocationID == employee.LocationID))
            {
                allEmployees.Remove(allEmployees.First(x => x.LocationID == employee.LocationID));
            }

            allEmployees.Add(employee);

            await Save("all", allEmployees);
        }
    }
}
