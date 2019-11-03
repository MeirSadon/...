using FlightManagementProject.DAO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public interface IAdministratorDAO : IBasicDB<Administrator>
    {
        IList<string> GetAllActionsHistory();
        IList<string> GetAllActionHistoryByDate(DateTime startDate, DateTime endDate);
        IList<string> GetAllActionHistoryByCategory(Categories category);
        IList<string> GetAllActionHistoryByIsSucceed(bool isSucceed);
    }
}
