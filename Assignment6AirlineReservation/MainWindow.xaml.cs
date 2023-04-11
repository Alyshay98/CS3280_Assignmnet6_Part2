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
                ResetSeats();
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

        /// <summary>
        /// Choose the passenger from the combo box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// Fill Passenger seats method
        /// </summary>
        private void FillPassengerSeats()
        {
            try
            {
                //Reset all seats in the selected flight to blue
                //Loop through each passenger in the list
                //Loop through each seat in the selected flight, like "c767_seats.Children"
                //Then compare the passengers seat to the label's content and if they match, then change the background to red because the seat is taken.
                ResetSeats();

                clsFlight selectedFlight = (clsFlight)(cbChooseFlight.SelectedItem);
                if (selectedFlight.sFlightID == "2")
                {

                    foreach (string SeatNum in clsFlightMan.GetFlightSeats(selectedFlight.sFlightID))
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
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Add passenger method
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// What happens when the passenger selection is changed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbChoosePassenger_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                
                if(cbChoosePassenger.SelectedIndex == -1)
                {
                    return;
                }

                ResetSeats();
                FillPassengerSeats();

                clsPassenger passenger = cbChoosePassenger.SelectedItem as clsPassenger;

                clsFlight selectedFlight = (clsFlight)(cbChooseFlight.SelectedItem);

                Label lblPassengerSeatNumber;


                if (selectedFlight.sFlightID == "1")
                {
                    lblPassengerSeatNumber = c767_Seats.FindName("Seat" + clsPassengerMan.GetPassengerSeat(selectedFlight.sFlightID, passenger.PassengerID)) as Label;
                    lblPassengerSeatNumber.Background = Brushes.Lime;
                    lblPassengersSeatNumber.Content = lblPassengerSeatNumber.Content.ToString();

                }
                else
                {
                    lblPassengerSeatNumber = cA380_Seats.FindName("SeatA" + clsPassengerMan.GetPassengerSeat(selectedFlight.sFlightID, passenger.PassengerID)) as Label;
                    lblPassengerSeatNumber.Background = Brushes.Lime;
                    lblPassengersSeatNumber.Content = lblPassengerSeatNumber.Content.ToString();
                }
                
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// Resets the seats to blue
        /// </summary>
        /// <exception cref="Exception"></exception>
        public void ResetSeats()
        {
            try
            {
                foreach (Label lblSeat in c767_Seats.Children)
                {
                    lblSeat.Background = Brushes.Blue;
                }

                foreach (Label lblSeat in cA380_Seats.Children)
                {
                    lblSeat.Background = Brushes.Blue;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(MethodInfo.GetCurrentMethod().DeclaringType.Name + "." +
                                    MethodInfo.GetCurrentMethod().Name + " -> " + ex.Message);
            }
        }

        /// <summary>
        /// Force the user to select a Seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdChangeSeat_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (cbChoosePassenger.SelectedIndex == -1)
                {
                    return;
                }
                //Passenger is selected
                clsPassenger passenger = cbChoosePassenger.SelectedItem as clsPassenger;
                //Lock down window and set bChangeSeatMode, force user to select a seat
                cbChooseFlight.IsEnabled = false;
                cbChoosePassenger.IsEnabled = false;
                gPassengerCommands.IsEnabled = false;
                lblPassengersSeatNumber.IsEnabled = false;
                cmdAddPassenger.IsEnabled = false;
                cmdChangeSeat.IsEnabled = false;
                cmdDeletePassenger.IsEnabled = false;
                gbColorKey.IsEnabled = false;

                bChangeSeatMode = true;
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
        }

        /// <summary>
        /// What happens when you click a seat
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Seat_Click(object sender, MouseButtonEventArgs e)
        {
            try
            {
                Label label = sender as Label;
                clsFlight currFlight = cbChooseFlight.SelectedItem as clsFlight;
                clsPassenger passenger = cbChoosePassenger.SelectedItem as clsPassenger;
                //This method will get called when a user clicks on any seat.
                //
                //What mode is the program in? bAddPassengerMode or bChangeSeatMode or regular seat selection
                //
                //bAddPassengerMode
                //Insert a new passenger into the database, then insert a record into the link table (Done in another class).
                if (bAddPassengerMode == true)
                {
                    clsPassengerMan.AddPassenger(wndAddPass.GetFirstName(), wndAddPass.GetLastName(), label.Content.ToString(),currFlight.sFlightID);
                    cbChoosePassenger.ItemsSource = clsPassengerMan.GetPassenger(currFlight.sFlightID);
                }
                bAddPassengerMode = false;
                //bChangeSeatMode
                //Only change the seat if the seat is empty (blue).
                //If it's empty then update the link table to update the user's new seat (Done in another class).
                if (bChangeSeatMode == true)
                {
                    if (!(label.Background == Brushes.Blue))
                    {
                        return;
                    }
                    else
                    {
                        clsPassengerMan.UpdatePassengerSeat(label.Content.ToString(), currFlight.sFlightID, passenger.PassengerID);
                        label.Background = Brushes.Lime;
                    }
                    FillPassengerSeats();
                    cbChooseFlight.IsEnabled = true;
                    cbChoosePassenger.IsEnabled = true;
                    gPassengerCommands.IsEnabled = true;
                    lblPassengersSeatNumber.IsEnabled = true;
                    cmdAddPassenger.IsEnabled = true;
                    cmdChangeSeat.IsEnabled = true;
                    cmdDeletePassenger.IsEnabled = true;
                    gbColorKey.IsEnabled = true;

                    bChangeSeatMode = false;
                }
                //Otherwise in regular seat selection:
                //If a seat is taken (red), then loop through the passengers in the combo box,
                //and keep looping until the seat that was clicked, its number matches a passenger's seat number,
                //then select that combo box index or selected item and put the passenger's seat in the label.
                if(bAddPassengerMode == false && bChangeSeatMode == false)
                {
                    //check the background of label only run the for loop if label.Background is red
                    if(label.Background == Brushes.Red)
                    {
                        foreach (clsPassenger passengers in cbChoosePassenger.Items)
                        {
                            if (clsPassengerMan.GetPassengerSeat(currFlight.sFlightID, passengers.PassengerID) == label.Content.ToString())
                            {
                                cbChoosePassenger.SelectedItem = passengers;
                            }
                            lblPassengersSeatNumber.Content = label.Content.ToString();
                        }
                    }                    
                }
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }              
        }

        /// <summary>
        /// Deletes a Passenger
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cmdDeletePassenger_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //Selected Passenger
                clsPassenger passenger = cbChoosePassenger.SelectedItem as clsPassenger;
                clsFlight currFlight = cbChooseFlight.SelectedItem as clsFlight;
                //Delete the currently selected passenger (Done in another class)
                clsPassengerMan.DeletePassenger(passenger.PassengerID, currFlight.sFlightID);
                //Reload the passengers into the combo box
                clsFlight selection = (clsFlight)cbChooseFlight.SelectedItem;
                cbChoosePassenger.ItemsSource = clsPassengerMan.GetPassenger(selection.sFlightID);
                //reload the taken seats
                FillPassengerSeats();
            }
            catch (Exception ex)
            {
                HandleError(MethodInfo.GetCurrentMethod().DeclaringType.Name,
                    MethodInfo.GetCurrentMethod().Name, ex.Message);
            }
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
