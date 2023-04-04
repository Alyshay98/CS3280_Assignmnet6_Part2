using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Assignment6AirlineReservation
{
    public class clsPassengerManager
    {
        /// <summary>
        /// Data Access Variable
        /// </summary>
        clsDataAccess dataAccess;

        /// <summary>
        /// Constructor That creates an object of Data Access
        /// </summary>
        public clsPassengerManager()
        {
            dataAccess = new clsDataAccess();
        }

        /// <summary>
        /// Method to get the Passengers
        /// Binds that data to method from the database
        /// </summary>
        /// <param name="sFlightID"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public List<clsPassenger> GetPassenger(string sFlightID)
        {
            try
            {
                DataSet ds = new DataSet();
                int iRet = 0;
                List<clsPassenger> Passengers = new List<clsPassenger>();
                string sSQL = clsSQL.GetPassengers(sFlightID);

                ds = dataAccess.ExecuteSQLStatement(sSQL, ref iRet);

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    clsPassenger passenger = new clsPassenger();
                    passenger.PassengerID = dr[0].ToString();
                    passenger.FirstName = dr[1].ToString();
                    passenger.LastName = dr[2].ToString();
                    Passengers.Add(passenger);
                }
                return Passengers;
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        //AddPassenger Method

        //UpdatePassengerSeat Method

        //DeletePassenger Method
    }
}

