using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsFlight
    {
        /// <summary>
        /// FlightID variable
        /// </summary>
        public string sFlightID;

        /// <summary>
        /// Flight Number Variable
        /// </summary>
        public string FlightNumber;

        /// <summary>
        /// AircraftType Variable
        /// </summary>
        public string AircraftType;

        /// <summary>
        /// Getter and Setter for Flights
        /// </summary>
        public int getsFlights { get; set; }

        /// <summary>
        /// Getter and Setter for Flight Number
        /// </summary>
        public int getFlightNumber { get; set; }

        /// <summary>
        /// Getter and Setter for AirCraftType
        /// </summary>
        public string getAircraftType { get; set; }

        /// <summary>
        /// Method that overrides the ToString Method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Concat(sFlightID, " - ", FlightNumber, AircraftType);
        }
    }
}
