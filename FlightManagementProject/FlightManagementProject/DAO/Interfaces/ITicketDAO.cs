using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public interface ITicketDAO : IBasicDB<Ticket>
    {
        IList<Ticket> GetTicketsByAirlineComapny(AirlineCompany airline);
        IList<Ticket> GetTicketsByCustomer(Customer customer);
    }
}
