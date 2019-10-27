using System;
using System.Collections.Generic;
using FlightManagementProject;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForAirlineFacade
    {
        /*  ======= All Tests =======
           
            1. CancelFlight           -- CancelFlightForCurrentAirline.
            2. ChangeMyPassword       -- ChangePasswordForAirline + WrongPasswordWhenTryChangePasswordForAirline.
            3. CreateFlight           -- GetAllTicketsForCurrentAirline + FlightIdIsNotMatchWhenTryToCreateFlight + DepartureTimeTooLateWhenTryToCreateFlight.
            4. GetAllFlightsByAirline -- GetAllTicketsForCurrentAirline.
            5. GetAllTicketsByAirline -- GetAllTicketsForCurrentAirline. 
            6. MofidyAirlineDetails   -- "TestForAdminFacadeClass"(UpdateAirline).
            7. UpdateFlight           -- UpdateFlightForCurrentAirline.

            ======= All Tests ======= */

        TestCenter tc = new TestCenter();


        // Try To Get All Tickets For Some Airline.
        [TestMethod]
        public void GetAllTicketsForCurrentAirline()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1), Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            tc.customerFacade.PurchaseTicket(tc.customerToken, flight);
            IList<Ticket> tickets = tc.airlineFacade.GetAllTicketsByAirline(tc.airlineToken);
            Assert.AreEqual(1, tickets.Count);
        }

        // Supposed To Get Exception Of "Flight Id Is Not Match".
        [TestMethod]
        [ExpectedException(typeof(FlightNotMatchException))]
        public void FlightIdIsNotMatchWhenTryToCreateFlight()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id+1, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1), Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
        }

        // Supposed To Get Exception Of "Departure Time Is Too Late".
        [TestMethod]
        [ExpectedException(typeof(DepartureTimeTooLateException))]
        public void DepartureTimeTooLateWhenTryToCreateFlight()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight(tc.airlineToken.User.Id, tc.adminFacade.GetCountryByName("Israel").Id, tc.adminFacade.GetCountryByName("Israel").Id, DateTime.Now + TimeSpan.FromHours(2), DateTime.Now + TimeSpan.FromHours(1), 10);
            flight.Remaining_Tickets = 0;
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
        }

        // Try To Cancel Flight For Current Airline.
        [TestMethod]
        public void CancelFlightForCurrentAirline()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight(tc.airlineToken.User.Id, tc.adminFacade.GetCountryByName("Israel").Id, tc.adminFacade.GetCountryByName("Israel").Id, new DateTime(2023, 12, 10), new DateTime(2023, 12, 11), 100);
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            IList<Flight> flights = tc.airlineFacade.GetAllFlights();
            Assert.AreEqual(1, flights.Count);
            tc.airlineFacade.CancelFlight(tc.airlineToken, flights[0]);
            flights = tc.airlineFacade.GetAllFlightsByAirline(tc.airlineToken);
            Assert.AreEqual(0, flights.Count);
        }

        // Try To Update Flight For Current Airline.
        [TestMethod]
        public void UpdateFlightForCurrentAirline()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1), Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            flight.Remaining_Tickets = 555;
            flight.Landing_Time = flight.Landing_Time + TimeSpan.FromDays(1);
            tc.airlineFacade.UpdateFlight(tc.airlineToken, flight);
            IList<Flight> flights = tc.airlineFacade.GetAllFlightsByAirline(tc.airlineToken);
            Assert.AreEqual(flights[0].Remaining_Tickets, 555);
        }

        // Change Password Successfuly For Airline.
        [TestMethod]
        public void ChangePasswordForAirline()
        {
            tc.PrepareDBForTests();
            tc.airlineFacade.ChangeMyPassword(tc.airlineToken, "123", "NewPassword");
            Assert.AreEqual(tc.airlineToken.User.Password, "NewPassword");
        }

        // Supposed To Get "WrongPasswordException" When Try Change Password For Airline.
        [TestMethod]
        [ExpectedException(typeof(WrongPasswordException))]
        public void WrongPasswordWhenTryChangePasswordForAirline()
        {
            tc.PrepareDBForTests();
            tc.airlineFacade.ChangeMyPassword(tc.airlineToken, "123456", "newPassword");
        }
    }
}
