using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    public interface ICountryDAO : IBasicDB<Country>
    {
         Country GetByName(string name);
    }
}
