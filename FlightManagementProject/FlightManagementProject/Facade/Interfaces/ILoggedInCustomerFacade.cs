using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    interface ILoggedInCustomerFacade
    {
        long PurchaseTicket(LoginToken<Customer> token, Flight flight);
        void CancelTicket(LoginToken<Customer> token, Ticket ticket);
        void MofidyCustomerDetails(LoginToken<Customer> token);
        void ChangeMyPassword(LoginToken<Customer> token, string oldPassword, string newPassword);
        IList<Flight> GetAllMyFlights(LoginToken<Customer> token);
        IList<Ticket> GetAllMyTickets(LoginToken<Customer> token);
        bool UserIsValid(LoginToken<Customer> token);
    }
}
