using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Assignment6AirlineReservation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// wndAddPassenger class variable
        /// </summary>
        wndAddPassenger wndAddPass;

        /// <summary>
        /// clsFlightManager class Variable
        /// </summary>
        clsFlightManager clsFlightMan;

        /// <summary>
        /// clsPassengerManager class Variable
        /// </summary>
        clsPassengerManager clsPassengerMan;

        /// <summary>
        /// Determines if we are adding a Passenger
        /// </summary>
        bool bAddPassengerMode;

        /// <summary>
        /// Determines if we are changing the seat
        /// </summary>
        bool bChangeSeatMode;

        public MainWindow()
        {
            try
            {
                InitializeComponent();
                Application.Current.ShutdownMode = ShutdownMode.OnMainWindowClose;
                CanvasA380.Visibility = Visibility.Collapsed;
                Canvas767.Visibility = Visibility.Collapsed;

                wndAddPass = new wndAddPassenger();
                clsFlightMan = new clsFlightManager();
                clsPassengerMan = new clsPassengerManager();

                cbChooseFlight.ItemsSource = clsFlightMan.GetFlights();

            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        private void cbChooseFlight_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsFlight selection = (clsFlight)cbChooseFlight.SelectedItem;
                cbChoosePassenger.IsEnabled = true;
                gPassengerCommands.IsEnabled = true;

                //Should be using a flight object to get the flight ID here
                if (selection.sFlightID == "1")
                {
                    CanvasA380.Visibility = Visibility.Hidden;
                    Canvas767.Visibility = Visibility.Visible;
                }
                else
                {
                    Canvas767.Visibility = Visibility.Hidden;
                    CanvasA380.Visibility = Visibility.Visible;
                }
                cbChoosePassenger.ItemsSource = clsPassengerMan.GetPassenger(selection.sFlightID);

                //FillPassengerSeats
                FillPassengerSeats();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        //FillPassengerSeatsMethod
        private void FillPassengerSeats()
        {
            //Reset all seats in the selected flight to blue
            //Loop through each passenger in the list
            //Loop through each seat in the selected flight, like "c767_seats.Children"
            //Then compare the passengers seat to the label's content and if they match, then change the background to red because the seat is taken.
            
            clsFlight selectedFlight = (clsFlight)(cbChooseFlight.SelectedItem);
            if(selectedFlight.sFlightID == "2")
            {
                
                foreach(string SeatNum in clsFlightMan.GetFlightSeats(selectedFlight.sFlightID))
                {
                    Label backgroundlbl = cA380_Seats.FindName("SeatA" + SeatNum) as Label;
                    backgroundlbl.Background = Brushes.Red;
                }
            }
            else
            {
                foreach (string SeatNum in clsFlightMan.GetFlightSeats(selectedFlight.sFlightID))
                {
                    Label backgroundlbl = c767_Seats.FindName("Seat" + SeatNum) as Label;
                    backgroundlbl.Background = Brushes.Red;
                }
            }
            
        }

        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();


                //check the Add Passenger window to see if the user clicked Save and if they did, then
                //Disable everything except the seats, so they are forced to click a seat.
                //Set the variable bAddPassengerMode that tells that the user is in Add Passenger mode

                if (wndAddPassenger.boolSaveClicked == true)
                {
                    cbChooseFlight.IsEnabled = false;
                    cbChoosePassenger.IsEnabled = false;
                    gPassengerCommands.IsEnabled = false;
                    lblPassengersSeatNumber.IsEnabled = false;
                    cmdAddPassenger.IsEnabled = false;
                    cmdChangeSeat.IsEnabled = false;
                    cmdDeletePassenger.IsEnabled = false;
                    gbColorKey.IsEnabled = false;

                    bAddPassengerMode = true;
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                clsPassenger selection = (clsPassenger)cbChoosePassenger.SelectedItem;
                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            //Passenger is selected
            //Lock down window and set bChangeSeatMode, force user to select a seat
        }

        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            //This method will get called when a user clicks on any seat.
            //
            //What mode is the program in? bAddPassengerMode or bChangeSeatMode or regular seat selection
            //
            //bAddPassengerMode
            //Insert a new passenger into the database, then insert a record into the link table (Done in another class).
            if (bAddPassengerMode == true)
            {
                
            }
            //bChangeSeatMode
            //Only change the seat if the seat is empty (blue).
            //If it's empty then update the link table to update the user's new seat (Done in another class).
            else if (bChangeSeatMode == true)
            {

            }
            //Otherwise in regular seat selection:
            //If a seat is taken (red), then loop through the passengers in the combo box,
            //and keep looping until the seat that was clicked, its number matches a passenger's seat number,
            //then select that combo box index or selected item and put the passenger's seat in the label.
            else
            {

            }  
        }

        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            //Selected Passenger
            //Delete the currently selected passenger (Done in another class)
            //Reload the passengers into the combo box
            //reload the taken seats
        }

        /// <summary>
        /// exception handler that shows the error
        /// </summary>
        /// <param name="sClass">the class</param>
        /// <param name="sMethod">the method</param>
        /// <param name="sMessage">the error message</param>
        private void HandleError(string sClass, string sMethod, string sMessage)
        {
            try
            {
                MessageBox.Show(sClass + "." + sMethod + " -> " + sMessage);
            }
            catch (System.Exception ex)
            {
                System.IO.File.AppendAllText(@"C:\Error.txt", Environment.NewLine + "HandleError Exception: " + ex.Message);
            }
        }
    }
}
