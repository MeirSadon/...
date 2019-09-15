using System;
using System.Collections.Generic;
using FlightManagementProject;
using FlightManagementProject.Facade;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TestForFlightManagmentProject
{
    [TestClass]
    public class TestForAnonymouseFacadeClass
    {
        /*  ======= All Tests =======

        1. GetAllAirlineCompanies         -- GetAllArlineCompanies.
        2. GetAllFlights                  -- GetAllFlights.
        3. GetAllFlightsVacancy           -- GetAllFlightsVacancy.
        4. GetFlightById                  -- GetFlightById.
        5. GetFlightsByDepatrureDate      -- GetFlightsByDepartureDate.
        6. GetFlightsByDestinationCountry -- GetFlightsByDestinationCountry.
        7. GetFlightsByLandingDate        -- GetFlightsByLandingDate.
        8. GetFlightsByOriginCountry      -- GetFlightsByOriginCountry.
        9. GetCountryByName               -- GetAllArlineCompanies.
        10. GetAirlineByName              -- GetAirlineCompanyByName

        ======= All Tests ======= */


        TestCenter tc = new TestCenter();

        // Supposed To Get All Airline Companies.
        [TestMethod]
        public void GetAllArlineCompanies()
        {
            tc.PrepareDBForTests();
            AirlineCompany airline = new AirlineCompany("AirlineForUpdate", "Airline " + tc.UserTest(), "123", (int)tc.adminFacade.GetCountryByName("Israel").Id);
            airline.Id = tc.adminFacade.CreateNewAirline(tc.adminToken, airline);
            IList<AirlineCompany> AirlineCompanies = new AnonymousUserFacade().GetAllAirlineCompanies();
            Assert.AreEqual(AirlineCompanies.Count, 2);
        }

        // Supposed To Get All Flights.
        [TestMethod]
        public void GetAllFlights()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1),
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 100 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            IList<Flight> flights = new AnonymousUserFacade().GetAllFlights();
            Assert.AreEqual(flights.Count, 1);
        }

        // Supposed To Get All Flights.
        [TestMethod]
        public void GetAllFlightsVacancy()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1), 
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 10 };
            Flight flight2 = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1),
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 0 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            flight2.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight2);
            Dictionary<Flight, int> TicketsByFlight = new AnonymousUserFacade().GetAllFlightsVacancy();
            Assert.AreEqual(TicketsByFlight.Count, 1);
        }

        // Supposed To Get Flight By Id.
        [TestMethod]
        public void GetFlightById()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = DateTime.Now + TimeSpan.FromHours(1),
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 10 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            Assert.AreEqual(tc.airlineFacade.GetFlightById((int)flight.Id), flight);
        }

        // Supposed To Get Flight By Departure Time.
        [TestMethod]
        public void GetFlightsByDepartureDate()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = DateTime.Now, Landing_Time = new DateTime(2019, 10, 08),
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 10 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            Assert.AreEqual(tc.airlineFacade.GetFlightsByDepartureDate(flight.Departure_Time).Count, 1);
        }

        // Supposed To Get Flight By Landing Time.
        [TestMethod]
        public void GetFlightsByLandingDate()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = new DateTime(2018, 10, 05), Landing_Time = new DateTime(2018, 10, 08),
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 10 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            Assert.AreEqual(tc.airlineFacade.GetFlightsByLandingDate(new DateTime(2018, 10, 08))[0], flight);
        }

        // Supposed To Get Flight By Destination Country.
        [TestMethod]
        public void GetFlightsByDestinationCountry()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = new DateTime(2018, 10, 05), Landing_Time = new DateTime(2018, 10, 08),
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 10 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            Assert.AreEqual(tc.adminFacade.GetFlightsByDestinationCountry((int)tc.adminFacade.GetCountryByName("Israel").Id).Count, 1);
        }

        // Supposed To Get Flight By Origin Country.
        [TestMethod]
        public void GetFlightsByOriginCountry()
        {
            tc.PrepareDBForTests();
            Flight flight = new Flight { AirLineCompany_Id = tc.airlineToken.User.Id, Departure_Time = new DateTime(2018, 10, 05), Landing_Time = new DateTime(2018, 10, 08),
                Origin_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Destination_Country_Code = tc.adminFacade.GetCountryByName("Israel").Id, Remaining_Tickets = 10 };
            flight.Id = tc.airlineFacade.CreateFlight(tc.airlineToken, flight);
            Assert.AreEqual(tc.adminFacade.GetFlightsByOriginCountry((int)tc.adminFacade.GetCountryByName("Israel").Id).Count, 1);
        }

        // Supposed To Get Airline Company By Name.
        [TestMethod]
        public void GetAirlineCompanyByUserName()
        {
            tc.PrepareDBForTests();
            AirlineCompany airline = new AnonymousUserFacade().GetAirlineByUserName(tc.airlineToken.User.User_Name);
            Assert.AreNotEqual(airline, null);
        }
    }
}
