using FlightManagementProject;
using FlightManagementProject.DAO;
using FlightManagementProject.Facade;
using FlightManagementProject.Poco_And_User;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject
{
    public class LoginService : ILoginService
    {
        private IAirlineDAO _airlineDAO = new AirlineDAOMSSQL();
        private ICustomerDAO _customerDAO = new CustomerDAOMSSQL();
        private IAdministratorDAO _administratorDAO = new AdministratorDAOMSSQL();
        IUserDAO _userDAO = new UserDAOMSSQL();

        public bool TryLogin(string userName, string password, out ILogin token, out FacadeBase facade)
        {
            token = null;
            facade = new AnonymousUserFacade();

            // Default Admin.
            if (userName.ToUpper() == FlyingCenterConfig.ADMIN_NAME.ToUpper())
            {
                if (password.ToUpper() == FlyingCenterConfig.ADMIN_PASSWORD.ToUpper())
                {
                    token = new LoginToken<Administrator>
                    {
                        User = new Administrator
                        ( 
                            0, //Admin Number 
                            0, //Id
                            FlyingCenterConfig.ADMIN_NAME,
                            FlyingCenterConfig.ADMIN_PASSWORD 
                        )
                    };
                    facade = new LoggedInAdministratorFacade();
                    return true;
                }
                else
                {
                    throw new WrongPasswordException("Sorry, But Your Password Isn't Match To Your User Name.");
                }
            }

            // DAO Users.
            User user = _userDAO.GetUserByUserName(userName);
            if (user != null)
            {
                if (password.ToUpper() == user.Password.ToUpper())
                {
                    switch (user.MyType)
                    {
                        case UserType.Administrator:
                            {
                                Administrator admin = _administratorDAO.GetById(user.Id);
                                token = new LoginToken<Administrator>
                                {
                                    User = new Administrator
                                    ( 
                                        admin.Admin_Number,
                                        user.Id,
                                        user.UserName,
                                        user.Password 
                                    )
                                };
                                facade = new LoggedInAdministratorFacade();
                                break;
                            }
                        case UserType.Airline:
                            {
                                AirlineCompany airline = _airlineDAO.GetById(user.Id);
                                token = new LoginToken<AirlineCompany>
                                {
                                    User = new AirlineCompany 
                                    (
                                        airline.Airline_Number, 
                                        user.Id, 
                                        user.UserName, 
                                        user.Password, 
                                        airline.Airline_Name, 
                                        airline.Country_Code 
                                    )
                                };
                                facade = new LoggedInAirlineFacade();
                                break;
                            }
                        case UserType.Customer:
                            {
                                Customer customer = _customerDAO.GetById(user.Id);
                                token = new LoginToken<Customer>
                                {
                                    User = new Customer
                                    (
                                        customer.Customer_Number,
                                        user.Id,
                                        user.UserName,
                                        user.Password,
                                        customer.First_Name,
                                        customer.Last_Name,
                                        customer.Address,
                                        customer.Phone_No,
                                        customer.Credit_Card_Number
                                    )
                                };
                                facade = new LoggedInCustomerFacade();
                                break;
                            }
                        default:
                            {
                                break;
                            }
                    }
                    return true;
                }
                else
                    throw new WrongPasswordException("Sorry, But Your Password Is Not Match To Your User Name.");                
            }
            
            return false;
        }
    }
}
