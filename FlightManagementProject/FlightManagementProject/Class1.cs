using FlightManagementProject.DAO;
using FlightManagementProject.Facade;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public class Class1
    {
        object o1 = FlyingCenterSystem.GetInstance();
        object o2 = FlyingCenterSystem.TryGetUserAndFacade("ad","ad",out ILogin user, out FacadeBase facade );
    }
}