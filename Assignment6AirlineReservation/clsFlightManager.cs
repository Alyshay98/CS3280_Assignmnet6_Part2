using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsFlightManager
    {
        /// <summary>
        /// Data Access Variable
        /// </summary>
        clsDataAccess dataAccess;

        /// <summary>
        /// Constructor with an object of Data Access
        /// </summary>
        public clsFlightManager()
        {
            dataAccess = new clsDataAccess();
        }


        /// <summary>
        /// method to get the flights
        /// </summary>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsFlight> GetFlights()
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;
                List<clsFlight> Flights = new List<clsFlight>();

                string sSQL = clsSQL.GetFlights();  //Get the SQL to get the flights
                ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);    //Execute the SQL to get the flights in a DataSet

                //Loop through each row in the DataSet and create a clsFlight object
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsFlight flights = new clsFlight();
                    flights.sFlightID = dr[0].ToString();
                    flights.FlightNumber = dr[1].ToString();
                    flights.AircraftType = dr[2].ToString();
                    Flights.Add(flights);
                }

                return Flights;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        public List<string> GetFlightSeats(string sFlightID)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;

                string sSQL = clsSQL.GetFlightSeats(sFlightID);
                ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);

                List<string> seats = new List<string>();

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    seats.Add(dr[0].ToString());
                }
                return seats;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }
    }
}
