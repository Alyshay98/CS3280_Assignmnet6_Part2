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
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        //FillPassengerSeatsMethod

        //Reset all seats in the selected flight to blue
        //Loop through each passenger in the list
        //Loop through each seat in teh selected flight, like "c767_seats.Children"
        //Then compare the passengers seat to the label's content and if they match, then change the background to red because the seat is taken.

        private void cmdAddPassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                wndAddPass = new wndAddPassenger();
                wndAddPass.ShowDialog();


                //check the Add Passenger window to see if the userr clicked Save and if they did, then
                //Disable  everything except the seats, so they are forced to click a seat.

                //Set the variable bAddPassengerMode that tells that the user is in Add Passenger mode
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

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

        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
