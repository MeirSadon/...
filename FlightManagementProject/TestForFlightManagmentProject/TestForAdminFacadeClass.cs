using System;
using FlightManagementProject;
using FlightManagementProject.DAO;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForAdminFacadeClass
    {
        /* ========   All Tests ========

           1. CreateNewAdmin                  -- "TestLogin Class" (LoginSuccesfullyAsDAOAdmin).
           2. CreateNewAirline                -- "TestLogin Class" (LoginSuccesfullyAsAirline).
           3. CreateNewCustomer               -- "TestLogin Class" (LoginSuccesfullyAsCustomer).
           4. CreateNewCountry                -- "TestCenter" (PrepareDBForTests).
           5. RemoveAdministrator             -- RemoveAdministratorSuccessfully + TryRemoveAdministratorUserThatNotExist.
           6. RemoveAirline                   -- RemoveAirlineSuccessfully + TryRemoveAirlineUserThatNotExist.
           7. RemoveCustomer                  -- RemoveCustomerSuccessfully + TryRemoveCustomerUserThatNotExist.
           8. RemoveCountry                   -- RemoveCustomerSuccessfully + TryRemoveCustomerUserThatNotExist.
           9. UpdateAirlineDetails            -- UpdateAirline.
           10. UpdateCustomerDetails          -- UpdateCustomer.
           11. UpdateCountryDetails           -- UpdateCountry.
           12. ChangeMyPassword               -- TryChangePasswordForAdministrator + WrongPasswordWhenTryChangePasswordForCentralAdmin + WrongPasswordWhenTryChangePasswordForSomeAdmin.
           13. ForceChangePasswordForAirline  -- ChangePasswordForSomeAirline.
           14. ForceChangePasswordForCustomer -- ChangePasswordForSomeCustomer.
           15. GetAirlineByUserName           -- UpdateAirline.
           16. GetCustomerByUserName          -- UpdateCustomer.

           17. GetAdminByUserName             -- GetAdminByUserName

           ========   All Tests ======== */

        private TestCenter tc = new TestCenter();


        // ===== Remove Successfully =====/

        // Remove Administrator Successfully.
        [TestMethod]
        public void RemoveAdministratorSuccessfully()
        {
            tc.PrepareDBForTests();
            Administrator admin = new Administrator("Admin: " + tc.UserTest(), "123");
            admin.Admin_Number = tc.adminFacade.CreateNewAdmin(tc.adminToken, admin);
            tc.adminFacade.RemoveAdministrator(tc.adminToken, admin);
            Assert.AreEqual(tc.adminFacade.GetAdminByUserName(tc.adminToken, admin.User_Name), null);
        }

        // Remove Airline Successfully.
        [TestMethod]
        public void RemoveAirlineSuccessfully()
        {
            tc.PrepareDBForTests();
            AirlineCompany airline = new AirlineCompany("AirlineForUpdate", "Airline " + tc.UserTest(), "123", (int)tc.adminFacade.GetCountryByName("Israel").Id);
            airline.Airline_Number = tc.adminFacade.CreateNewAirline(tc.adminToken, airline);
            tc.adminFacade.RemoveAirline(tc.adminToken, airline);
            Assert.AreEqual(tc.adminFacade.GetAirlineByUserName(tc.adminToken, airline.User_Name), null);
        }

        // Remove Customer Successfully.
        [TestMethod]
        public void RemoveCustomerSuccessfully()
        {
            tc.PrepareDBForTests();
            Customer customer = new Customer("Shiran", "Ben Sadon", tc.UserTest(), "123", "Neria 28", "050", "3317");
            customer.Customer_Number = tc.adminFacade.CreateNewCustomer(tc.adminToken, customer);
            tc.adminFacade.RemoveCustomer(tc.adminToken, customer);
            Assert.AreEqual(tc.adminFacade.GetCustomerByUserName(tc.adminToken, customer.User_Name), null);
        }

        // Remove Country Successfully.
        [TestMethod]
        public void RemoveCountrySuccessfully()
        {
            tc.PrepareDBForTests();
            tc.adminFacade.RemoveCountry(tc.adminToken, tc.adminFacade.GetCountryByName("Israel"));
            Assert.AreEqual(tc.adminFacade.GetCountryByName("Israel"), null);
        }
        

        // ===== Get "UserNotExist" When Try Remove =====//

        // Supposed To Get "UserNotExistException" When Try Remove Administrator.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveAdministratorUserThatNotExist()
        {
            tc.PrepareDBForTests();
            Administrator admin = new Administrator("Admin: " + tc.UserTest(), "123");
            tc.adminFacade.RemoveAdministrator(tc.adminToken, admin);
        }

        // Supposed To Get "UserNotExistException" When Try Remove Airline.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveAirlineUserThatNotExist()
        {
            tc.PrepareDBForTests();
            AirlineCompany airline = new AirlineCompany("AirlineForUpdate", "Airline " + tc.UserTest(), "123", (int)tc.adminFacade.GetCountryByName("Israel").Id);
            tc.adminFacade.RemoveAirline(tc.adminToken, airline);
        }
        
        // Supposed To Get "UserNotExistException" When Try Remove Customer.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveCustomerUserThatNotExist()
        {
            tc.PrepareDBForTests();
            Customer customer = new Customer("Shiran", "Ben Sadon", tc.UserTest(), "123", "Neria 28", "050", "3317");
            tc.adminFacade.RemoveCustomer(tc.adminToken, customer);
        }

        // Supposed To Get "UserNotExistException" When Try Remove Country.
        [TestMethod]
        [ExpectedException(typeof(UserNotExistException))]
        public void TryRemoveCountryUserThatNotExist()
        {
            tc.PrepareDBForTests();
            Country country = new Country("USA");
            tc.adminFacade.RemoveCountry(tc.adminToken, country);
        }

        // ===== Update Details ===== //

        // Update Details For Airline Company.
        [TestMethod]
        public void UpdateAirline()
        {
            tc.PrepareDBForTests();
            AirlineCompany airline = new AirlineCompany("AirlineForUpdate", "Airline " + tc.UserTest(), "123", (int)tc.adminFacade.GetCountryByName("Israel").Id);
            airline.Airline_Number = tc.adminFacade.CreateNewAirline(tc.adminToken, airline);
            airline.Airline_Name = "CHANGED!";
            tc.adminFacade.UpdateAirlineDetails(tc.adminToken, airline);
            Assert.AreEqual(tc.adminFacade.GetAirlineByUserName(tc.adminToken, airline.User_Name).Airline_Name, "CHANGED!");
        }

        // Update Details For Customer.
        [TestMethod]
        public void UpdateCustomer()
        {
            tc.PrepareDBForTests();
            Customer customer = new Customer("Shiran", "Ben Sadon", tc.UserTest(), "123","Neria 28","050","3317" );
            customer.Customer_Number = tc.adminFacade.CreateNewCustomer(tc.adminToken, customer);
            customer = tc.adminFacade.GetCustomerByUserName(tc.adminToken, customer.User_Name);
            customer.First_Name = "CHANGED!";
            tc.adminFacade.UpdateCustomerDetails(tc.adminToken, customer);
            Assert.AreEqual(tc.adminFacade.GetCustomerByUserName(tc.adminToken, customer.User_Name).First_Name, "CHANGED!");
        }

        // Update Details For Country.
        [TestMethod]
        public void UpdateCountry()
        {
            tc.PrepareDBForTests();
            Country country = new Country("USA");
            country.Id = tc.adminFacade.CreateNewCountry(tc.adminToken, country);
            country.Country_Name = "China";
            tc.adminFacade.UpdateCountryDetails(tc.adminToken, country);
            Assert.AreEqual(tc.adminFacade.GetCountryByName(country.Country_Name).Country_Name, "China");
        }


        //  Change Password Succesfully =====//

        // Try Change Password Successfuly For Administrator.
        [TestMethod]
        public void TryChangePasswordForAdministrator()
        {
            tc.PrepareDBForTests();
            Administrator admin = new Administrator("Admin: " + tc.UserTest(), "123");
            tc.adminFacade.CreateNewAdmin(tc.adminToken, admin);
            FlyingCenterSystem.TryGetUserAndFacade(admin.User_Name, admin.Password, out ILogin token, out FacadeBase facade);
            LoginToken<Administrator> newToken = token as LoginToken<Administrator>;
            LoggedInAdministratorFacade newFacade = facade as LoggedInAdministratorFacade;
            newFacade.ChangeMyPassword(newToken, "123".ToUpper(), "newPassword".ToUpper());
            Assert.AreEqual(newToken.User.Password.ToUpper(), "newPassword".ToUpper());
        } 
        
        // Change Password Successfuly For Airline Company.
        [TestMethod]
        public void ChangePasswordForSomeAirline()
        {
            tc.PrepareDBForTests();
            AirlineCompany airline = new AirlineCompany("AirlineForUpdate", "Airline " + tc.UserTest(), "123", (int)tc.adminFacade.GetCountryByName("Israel").Id);
            airline.Airline_Number = tc.adminFacade.CreateNewAirline(tc.adminToken, airline);
            tc.adminFacade.ForceChangePasswordForAirline(tc.adminToken,airline, "newPassword".ToUpper());
            Assert.AreEqual(tc.adminFacade.GetAirlineByUserName(tc.adminToken, airline.User_Name).Password, "newPassword".ToUpper());
        }

        // Change Password Successfuly For Customer.
        [TestMethod]
        public void ChangePasswordForSomeCustomer()
        {
            tc.PrepareDBForTests();
            Customer customer = new Customer("Shiran", "Ben Sadon", tc.UserTest(), "123", "Neria 28", "050", "3317");
            customer.Customer_Number = tc.adminFacade.CreateNewCustomer(tc.adminToken, customer);
            tc.adminFacade.ForceChangePasswordForCustomer(tc.adminToken, customer, "newPassword".ToUpper());
            Assert.AreEqual(tc.adminFacade.GetCustomerByUserName(tc.adminToken, customer.User_Name).Password, "newPassword".ToUpper());
        }

        // ===== Get "WrongPasswordException" When Try Change Password =====//

        // Supposed To Get "WrongPasswordException" When Try Change Password For Central Administrator.
        [TestMethod]
        [ExpectedException(typeof(CentralAdministratorActionsException))]
        public void WrongPasswordWhenTryChangePasswordForCentralAdmin()
        {
            tc.PrepareDBForTests();
            tc.adminFacade.ChangeMyPassword(tc.adminToken, "123456", "newPassword");
        }

        // Supposed To Get "WrongPasswordException" When Try Change Password For Some Administrator.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryChangePasswordForSomeAdmin()
        {
            tc.PrepareDBForTests();
            Administrator admin = new Administrator ( "Admin: " + tc.UserTest(), "123" );
            tc.adminFacade.CreateNewAdmin(tc.adminToken, admin);
            FlyingCenterSystem.TryGetUserAndFacade(admin.User_Name, admin.Password, out ILogin token, out FacadeBase facade);
            LoginToken<Administrator> newToken = token as LoginToken<Administrator>;
            LoggedInAdministratorFacade newFacade = facade as LoggedInAdministratorFacade;
            tc.adminFacade.ChangeMyPassword(newToken, "123345", "newPassword");
        }


        // ===== Search Functions =====//

        // Search Some Admin By User Name.
        [TestMethod]
        public void GetAdminByUserName()
        {
            tc.PrepareDBForTests();
            Administrator admin = new Administrator("Admin: " + tc.UserTest(), "123");
            admin.Admin_Number = tc.adminFacade.CreateNewAdmin(tc.adminToken, admin);
            Assert.AreNotEqual(tc.adminFacade.GetAdminByUserName(tc.adminToken, admin.User_Name), null);
        }

        // Search Some Airline By User Name.
        [TestMethod]
        public void GetAirlineByUserName()
        {
            tc.PrepareDBForTests();
            AirlineCompany airline = new AirlineCompany("AirlineForUpdate", "Airline " + tc.UserTest(), "123", (int)tc.adminFacade.GetCountryByName("Israel").Id);
            airline.Airline_Number = tc.adminFacade.CreateNewAirline(tc.adminToken, airline);
            Assert.AreNotEqual(tc.adminFacade.GetAirlineByUserName(tc.adminToken, airline.User_Name), null);
        }

        // Search Some Customer By User Name.
        [TestMethod]
        public void GetCustomerByUserName()
        {
            tc.PrepareDBForTests();
            Customer customer = new Customer("Shiran", "Ben Sadon", tc.UserTest(), "123", "Neria 28", "050", "3317");
            customer.Customer_Number = tc.adminFacade.CreateNewCustomer(tc.adminToken, customer);
            Assert.AreNotEqual(tc.adminFacade.GetCustomerByUserName(tc.adminToken, customer.User_Name), null);
        }
    }
}
