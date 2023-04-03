using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsPassenger
    {
        /// <summary>
        /// Passenger ID Variable
        /// </summary>
        public string PassengerID;

        /// <summary>
        /// Passenger's First Name Variable
        /// </summary>
        public string FirstName;

        /// <summary>
        /// Passenger's Last Name Variable
        /// </summary>
        public string LastName;

        /// <summary>
        /// Getter and Setter for Passenger ID
        /// </summary>
        public string getPassengerID { get; set; }

        /// <summary>
        /// Getter and Setter for Passenger First Name
        /// </summary>
        public string getFirstName { get; set; }

        /// <summary>
        /// Getter and Setter for Passenger Last Name
        /// </summary>
        public string getLastName { get; set; }


        /// <summary>
        /// Overrides the ToString Method
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return String.Concat(PassengerID, " - ", FirstName, LastName);
        }
    }
}

