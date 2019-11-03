using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlightManagementProject.Facade;

namespace FlightManagementProject
{
    public class LoginToken<T> : ILogin where T : IUser
    {
        public T User { get; set; }
    }
}
