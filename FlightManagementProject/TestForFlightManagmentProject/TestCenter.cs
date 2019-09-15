using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using FlightManagementProject.Facade;
using FlightManagementProject;

namespace TestForFlightManagmentProject
{
    public class TestCenter
    {
        public LoggedInAdministratorFacade adminFacade;
        public LoginToken<Administrator> adminToken;
        public LoggedInAirlineFacade airlineFacade;
        public LoginToken<AirlineCompany> airlineToken;
        public LoggedInCustomerFacade customerFacade;
        public LoginToken<Customer> customerToken;

        public TestCenter()
        {
            adminFacade = new LoggedInAdministratorFacade();
            adminToken = new LoginToken<Administrator> { User = new Administrator(FlyingCenterConfig.ADMIN_NAME, FlyingCenterConfig.ADMIN_PASSWORD ) };

            adminFacade.CreateNewCountry(adminToken, new Country() { Country_Name = "Israel", });


            airlineFacade = new LoggedInAirlineFacade();
            airlineToken = new LoginToken<AirlineCompany> { User = new AirlineCompany("TestAirline", "Airline " + UserTest(), "123", (int)adminFacade.GetCountryByName("Israel").Id)};
            adminFacade.CreateNewAirline(adminToken, airlineToken.User);
            airlineToken.User = adminFacade.GetAirlineByUserName(adminToken, airlineToken.User.User_Name);

            customerFacade = new LoggedInCustomerFacade();
            customerToken = new LoginToken<Customer> { User = new Customer("TestCustomer", "Ben Sadon", UserTest(), "123", "Neria 28", "050", "3317") };
            adminFacade.CreateNewCustomer(adminToken, customerToken.User);
            customerToken.User = adminFacade.GetCustomerByUserName(adminToken, customerToken.User.User_Name);
        }
        // Create Random User Names For Tests.
        public string UserTest()
        {
            return "Test" + new Random().Next(100000);
        }

        //Remove All The Tables In DataBase And Add New Country.
        public void PrepareDBForTests()
        {
            using (SqlConnection conn = new SqlConnection(FlyingCenterConfig.CONNECTION_STRING))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("Delete From Tickets;" +
                    "Delete From Flights;" +
                    "Delete From AirlineCompanies;" +
                    "Delete From Customers;" +
                    "Delete From Countries;" +
                    "Delete From Administrators;" +
                    "Delete From Users", conn))
                {
                    cmd.ExecuteNonQuery();
                }
            }
            adminFacade.CreateNewCountry(adminToken, new Country() { Country_Name = "Israel", });
            adminFacade.CreateNewAirline(adminToken, airlineToken.User);
            adminFacade.CreateNewCustomer(adminToken, customerToken.User);
        }
    }
}
