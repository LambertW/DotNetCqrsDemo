using RestSharp;
using System;

namespace DotNetCqrsDemo.Initializer
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new RestClient("http://localhost:53958/api/");

            #region Locations

            var request = new RestRequest("locations/create", Method.POST);
            request.AddParameter("LocationID", 1);
            request.AddParameter("StreetAddress", "1234 S Main St");
            request.AddParameter("PostalCode", "Anywhere");
            request.AddParameter("State", "KS");
            request.AddParameter("City", "67203");

            client.Execute(request);

            request = new RestRequest("locations/create", Method.POST);
            request.AddParameter("LocationID", 2);
            request.AddParameter("StreetAddress", "578 S Central Avd");
            request.AddParameter("PostalCode", "Anywhere");
            request.AddParameter("State", "KS");
            request.AddParameter("City", "67203");

            client.Execute(request);
            #endregion

            #region Employee

            request = new RestRequest("employee/create", Method.POST);
            request.AddParameter("EmployeeID", 1);
            request.AddParameter("FirstName", "Honh");
            request.AddParameter("LastName", "Smith");
            request.AddParameter("DateOfBirth", new DateTime(1983, 12, 1));
            request.AddParameter("JobTitle", "General Manager");
            request.AddParameter("LocationID", "0");

            client.Execute(request);

            request = new RestRequest("employee/create", Method.POST);
            request.AddParameter("EmployeeID", 2);
            request.AddParameter("FirstName", "Maggie");
            request.AddParameter("LastName", "Franks");
            request.AddParameter("DateOfBirth", new DateTime(1990, 12, 1));
            request.AddParameter("JobTitle", "Line Cook");
            request.AddParameter("LocationID", "0");

            client.Execute(request);

            request = new RestRequest("employee/create", Method.POST);
            request.AddParameter("EmployeeID", 3);
            request.AddParameter("FirstName", "Reggie");
            request.AddParameter("LastName", "Martines");
            request.AddParameter("DateOfBirth", new DateTime(1992, 12, 1));
            request.AddParameter("JobTitle", "Shift Manager");
            request.AddParameter("LocationID", "0");

            client.Execute(request);

            #endregion

            #region Assign

            #endregion
        }
    }
}
