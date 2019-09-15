using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public interface ICustomerDAO : IBasicDB<Customer>
    {
        void ChangePassword(Customer customer);
    }
}
