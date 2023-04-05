using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    internal class clsSQL
    {
        /// <summary>
        /// SQL method to Get the flights
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static string GetFlights()
        {
            try
            {
                string sSQL = "SELECT Flight_ID, Flight_Number, Aircraft_Type FROM FLIGHT";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }


        /// <summary>
        /// SQL method to get the Passengers
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static String GetPassengers(string sFlightID)
        {
            try
            {
                string sSQL = "SELECT PASSENGER.Passenger_ID, First_Name, Last_Name, Seat_Number " +
                              "FROM FLIGHT_PASSENGER_LINK, FLIGHT, PASSENGER " +
                              "WHERE FLIGHT.FLIGHT_ID = FLIGHT_PASSENGER_LINK.FLIGHT_ID AND " +
                              "FLIGHT_PASSENGER_LINK.PASSENGER_ID = PASSENGER.PASSENGER_ID AND " +
                              "FLIGHT.FLIGHT_ID = " + sFlightID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static String UpdateSeatNumbers()
        {
            try
            {
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                              "SET Seat_Number = '5' " +
                              "WHERE FLIGHT_ID = 1 AND PASSENGER_ID = 2";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public static String InsertPassenger()
        {
            string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('FirstName','LastName')";

            return sSQL;
        }

        public static string InsertIntoLinkTable()
        {
            string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                          "VALUES( 1 , 6 , 3)";
            return sSQL;
        }

        public static String DeleteLink()
        {
            string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                          "WHERE FLIGHT_ID = 1 AND " +
                          "PASSENGER_ID = 6";
            return sSQL;
        }

        public static string DeletePassengers()
        {
            string sSQL = "Delete FROM PASSENGER " +
                          "WHERE PASSENGER_ID = 6";
            return sSQL;
        }

        public static String InsertNewPassenger()
        {
            string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('FirstName','LastName')";

            sSQL = "SELECT Passenger_ID from Passenger where First_Name = 'Shawn' AND Last_Name = 'Cowder'";
            return sSQL;
        } 
    }
}
