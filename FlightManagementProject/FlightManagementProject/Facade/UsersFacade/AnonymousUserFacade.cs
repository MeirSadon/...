using FlightManagementProject.DAO;
using FlightManagementProject.Poco_And_User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.Facade
{
    // Basic Facade(Without Login) With The Simple Options.
    public class AnonymousUserFacade : FacadeBase, IAnonymousUserFacade
    {

        // Get Some Airline Company By User Name.
        public AirlineCompany GetAirlineByUserName(string userName)
        {
            AirlineCompany airline = null;
                User airlineUser = _userDAO.GetUserByUserName(userName);
                if (airlineUser != null)
                {
                    airline = _airlineDAO.GetById(airlineUser.Id);
                }
            return airline;
        }
        
        // Get All Airline Companies.
        public IList<AirlineCompany> GetAllAirlineCompanies()
        {
            return _airlineDAO.GetAll();
        }


        // Get Some Flight By Id.
        public Flight GetFlightById(int id)
        {
            return _flightDAO.GetById(id);
        }
        
        // Get All Flights With At Least One Ticket.
        public Dictionary<Flight, int> GetAllFlightsVacancy()
        {
            return _flightDAO.GetAllFlightsVacancy();
        }

        //Get All Flights By Departure Time.
        public IList<Flight> GetFlightsByDepartureDate(DateTime departureDate)
        {
            return _flightDAO.GetFlightsByDepartureDate(departureDate);
        }

        //Get All Flights By Destination Country.
        public IList<Flight> GetFlightsByDestinationCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByDestinationCountry(countryCode);
        }

        // Get All Flights By Landing Date.
        public IList<Flight> GetFlightsByLandingDate(DateTime landingDate)
        {
            return _flightDAO.GetFlightsByLandingDate(landingDate);
        }

        // Get All Flights By Origin Country.
        public IList<Flight> GetFlightsByOriginCountry(int countryCode)
        {
            return _flightDAO.GetFlightsByOriginCounty(countryCode);
        }
        
        // Get All Flights
        public IList<Flight> GetAllFlights()
        {
            return _flightDAO.GetAll();
        }


        //Get Country By Id.
        public Country GetCountryById(int id)
        {
            return _countryDAO.GetById(id);
        }
        
        // Get Country By Name.
        public Country GetCountryByName(string name)
        {
            return _countryDAO.GetByName(name);
        }

        //Get All Countries.
        public IList<Country> GetAllCountries()
        {
            return _countryDAO.GetAll();
        }
    }
}
