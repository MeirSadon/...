using System;
using System.Collections.Generic;
using FlightManagementProject;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForCustomerFacade
    {
        /*  ======= All Tests =======

        1. CancelTicket          -- CancelTicketSuccessfuly + TooLateToCancelTicketWhenTryCancelTicket.
        2. GetAllMyTickets       -- CancelTicketSuccessfuly.
        3. GetAllMyFlights       -- CancelTicketSuccessfuly.
        4. PurchaseTicket        -- CancelTicketSuccessfuly.
        5. MofidyCustomerDetails -- "TestForAdminFacadeClass"(UpdateCustomer).
        6. ChangeMyPassword      -- ChangePasswordForCustomer + WrongPasswordWhenTryChangePasswordForCustomer.

        ======= All Tests ======= */

        TestCenter tc = new TestCenter();

        [TestMethod]
        public void CancelTicketSuccessfuly()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = new DateTime(2020,12,10, 00, 00, 00), Landing_Time = new DateTime(2020, 12, 11), Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            tc.customerFacade.PurchaseTicket(tc.customerToken, flight);
            Assert.AreEqual(tc.customerFacade.GetAllMyFlights(tc.customerToken).Count, 1);
            tc.customerFacade.CancelTicket(tc.customerToken, tc.customerFacade.GetAllMyTickets(tc.customerToken)[0]);
            Assert.AreEqual(tc.customerFacade.GetAllMyFlights(tc.customerToken).Count, 0);
        }

        //Supposed To Get "TooLateToCancelTicket" Exception.
        [TestMethod]
        [ExpectedException(typeof(TooLateToCancelTicketException))]
        public void TooLateToCancelTicketWhenTryCancelTicket()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = new DateTime(2019, 07, 05), Landing_Time = new DateTime(2019, 11, 05), Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            tc.customerFacade.PurchaseTicket(tc.customerToken, flight);
            tc.customerFacade.CancelTicket(tc.customerToken, tc.customerFacade.GetAllMyTickets(tc.customerToken)[0]);
        }

        // Change Password Successfuly For Customer.
        [TestMethod]
        public void ChangePasswordForCustomer()
        {
            tc.PrepareDBForTests();
            tc.customerFacade.ChangeMyPassword(tc.customerToken, tc.customerToken.User.Password, "NewPassword");
            Assert.AreEqual(tc.customerToken.User.Password, "NewPassword");
        }

        // Supposed To Get "WrongPasswordException" When Try Change Password For Customer.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryChangePasswordForCustomer()
        {
            tc.PrepareDBForTests();
            tc.customerFacade.ChangeMyPassword(tc.customerToken, "123456", "newPassword");
        }
    }
}
