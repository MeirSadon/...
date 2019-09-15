using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO.Interfaces
{
    public interface IBackgroundDAO
    {
        void AddNewAction(Categories category, string action, bool isSucceed);
        void UpdateTicketsAndFlightsHistory();
    }
}
