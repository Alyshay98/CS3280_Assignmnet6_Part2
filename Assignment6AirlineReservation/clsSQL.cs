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
        /// SQL method to get the flight seats
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static String GetFlightSeats(string sFlightID)
        {
            try
            {
                string sSQL = "Select Seat_Number from FLIGHT_PASSENGER_LINK Where flight_ID = " + sFlightID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL method to get the Passenger Seat
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="sPassID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static String GetPassengerSeat(string sFlightID, string sPassID)
        {
            try
            {
                string sSQL = "Select Seat_Number from FLIGHT_PASSENGER_LINK Where flight_ID = " + sFlightID + " AND Passenger_ID = " + sPassID;
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

        /// <summary>
        /// SQL method to update the seat Number
        /// </summary>
        /// <param name="lblSeat"></param>
        /// <param name="sFlightID"></param>
        /// <param name="sPassID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static String UpdateSeatNumbers(string lblSeat, string sFlightID, string sPassID)
        {
            try
            {
                string sSQL = "UPDATE FLIGHT_PASSENGER_LINK " +
                              "SET Seat_Number = " + lblSeat +
                              " WHERE FLIGHT_ID = " + sFlightID + " AND PASSENGER_ID = " + sPassID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL method to Insert Passenger
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <returns></returns>
        public static String InsertPassenger(string FirstName, string LastName)
        {
            try
            {
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('" + FirstName + "', '" + LastName + "')";

                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL Method to Insert into link table
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="PassID"></param>
        /// <param name="sSeatNum"></param>
        /// <returns></returns>
        public static string InsertIntoLinkTable(string sFlightID, string PassID, string sSeatNum)
        {
            try
            {
                string sSQL = "INSERT INTO Flight_Passenger_Link(Flight_ID, Passenger_ID, Seat_Number) " +
                          "VALUES(" + sFlightID + " , " + PassID + ", " + sSeatNum + ")";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL method to Delete the Link
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <param name="PassID"></param>
        /// <returns></returns>
        public static String DeleteLink(string sFlightID, string PassID)
        {
            try
            {

                string sSQL = "Delete FROM FLIGHT_PASSENGER_LINK " +
                              "WHERE FLIGHT_ID = " + sFlightID +
                              " AND PASSENGER_ID = " + PassID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// SQL method to delete passenger
        /// </summary>
        /// <param name="PassID"></param>
        /// <returns></returns>
        public static string DeletePassenger(string PassID)
        {
            try
            {
                string sSQL = "Delete FROM PASSENGER " +
                          "WHERE PASSENGER_ID = " + PassID;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            } 
        }

        /// <summary>
        /// SQL method to Insert a new Passenger
        /// </summary>
        /// <param name="FirstName"></param>
        /// <param name="LastName"></param>
        /// <returns></returns>
        /*public static String InsertNewPassenger(string FirstName, string LastName)
        {
            try
            {
                string sSQL = "INSERT INTO PASSENGER(First_Name, Last_Name) VALUES('" + FirstName + "', '" + LastName + "')";

                sSQL = "SELECT Passenger_ID from Passenger where First_Name = " + FirstName + " AND Last_Name = " + LastName;
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }*/

        public static String GetNewPassengersID(string FirstName, string LastName)
        {
            try
            {
                string sSQL = "SELECT Passenger_ID from Passenger where First_Name = '" + FirstName + "' AND Last_Name = '" + LastName + "'";
                return sSQL;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
