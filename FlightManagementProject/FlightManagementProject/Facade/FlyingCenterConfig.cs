using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    public static class FlyingCenterConfig
    {
        public const string ADMIN_NAME = "ADMIN";
        public const string ADMIN_PASSWORD = "9999";

        // Azure Connection.
        //public const string CONNECTION_STRING = @"Server=tcp:flight-managment-srv.database.windows.net,1433;Initial Catalog=FlightManagmentDB;Persist Security Info=False;User ID=meir;Password=Password1!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        
        // My Computer Connection.
        public const string CONNECTION_STRING = @"Data Source=MEIR-PC\SQLEXPRESS ;Initial Catalog = FlightManagement; Integrated Security = true";

        public const int TIMEFORTHREADHISTORY = 1000 * 3600 * 24;

    }
}
