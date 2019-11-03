using FlightManagementProject.DAO;
using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    interface ILoginService
    {
        //bool TryAdminLogin(string userName, string password, out LoginToken<Administrator> token);
        //bool TryCustomerLogin(string userName, string password, out LoginToken<Customer> token);
        //bool TryAirlineLogin(string userName, string password, out LoginToken<AirlineCompany> token);
        bool TryLogin(string userName, string password, out ILogin token, out FacadeBase facade);
    }
}
