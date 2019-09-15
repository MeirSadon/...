using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Poco_And_User
{
    public class User : IUser
    {
        public long Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public UserType MyType { get; set; }

        //Empty Constractor.
        public User()
        {

        }

        //Constractor Without Id.
        public User(string userName, string password, UserType type)
        {
            UserName = userName;
            Password = password;
            MyType = type;
        }

        //Full Constractor.
        public User(long id, string userName, string password, UserType myType)
        {
            Id = id;
            UserName = userName;
            Password = password;
            MyType = myType;
        }
    }
}
