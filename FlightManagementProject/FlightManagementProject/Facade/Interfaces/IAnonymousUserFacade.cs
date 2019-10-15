using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightManagementProject.DAO
{
    // Interface For Anonymous User (And Over).
    public interface IAnonymousUserFacade
    {
        AirlineCompany GetAirlineByUserName(string name);
        IList<AirlineCompany> GetAllAirlineCompanies();

        Flight GetFlightById(int id);
        Dictionary<Flight, int> GetAllFlightsVacancy();
        IList<Flight> GetFlightsByOriginCountry(int countryCode);
        IList<Flight> GetFlightsByDestinationCountry(int countryCode);
        IList<Flight> GetFlightsByDepartureDate(DateTime departureDate);
        IList<Flight> GetFlightsByLandingDate(DateTime landingDate);
        IList<Flight> GetAllFlights();

        Country GetCountryById(int id);
        Country GetCountryByName(string userName);
        IList<Country> GetAllCountries();
    }
}
