using RestSharp;
using System;

namespace DotNetCqrsDemo.Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:53958/api/");

            var request = new RestRequest("employee/create", Method.POST);
            request.AddParameter("EmployeeID", 1);
            request.AddParameter("FirstName", "Honh");
            request.AddParameter("LastName", "Smith");
            request.AddParameter("DateOfBirth", new DateTime(1983, 12, 1));
            request.AddParameter("JobTitle", "General Manager");

            var response = client.Execute(request);
        }
    }
}
