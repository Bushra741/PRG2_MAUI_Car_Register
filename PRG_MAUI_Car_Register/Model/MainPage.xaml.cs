using PRG_MAUI_Car_Register.Model;

namespace PRG_MAUI_Car_Register
{
    public partial class MainPage : ContentPage
    {
        List<Vehicle> vehicleList = new List<Vehicle>();

        public MainPage()
        {
            InitializeComponent();
            pickerType.SelectedIndex = 0;

            entryModelYear.ItemsSource =
            Enumerable.Range(1895, DateTime.Today.Year - 1895 + 1).ToList();

            entryModelYear.SelectedItem = DateTime.Today.Year;
        }

        private void OnRegisterClicked(object sender, EventArgs e)
        {
           
                try
                {
                    Vehicle vehicle = new Vehicle((Vehicle.Type)pickerType.SelectedIndex);

                    vehicle.RegistrationNumber = entryRegistrationNumber.Text;
                    vehicle.Manufacturer = entryManufacturer.Text;
                    vehicle.Model = entryModel.Text;

                    // Safely parse the year from the Entry text field
                    if (int.TryParse(entryModelYear.Text, out int year))
                    {
                        vehicle.Year = year;
                    }
                    else
                    {
                        DisplayAlert("Fel", "Årtalet måste vara ett giltigt heltal.", "OK");
                        return;
                    }

                    vehicleList.Add(vehicle);
                    listViewVehicles.ItemsSource = null;
                    listViewVehicles.ItemsSource = vehicleList;

                    ClearTextFields();
                }
                catch (ArgumentException ex)
                {
                    DisplayAlert("Fel", ex.Message, "OK");
                
            }
        }
        private void OnRadioCheckedChanged(object sender, CheckedChangedEventArgs e)
        {
            if (e.Value != true) return;

            // Skapa en temporär filtrerad lista baserat på vilken radioknapp som är vald
            List<Vehicle> filteredList;

            if (radioCar.IsChecked)
            {
                filteredList = vehicleList
                    .Where(v => v.VehicleType == "Bil")
                    .ToList();
            }
            else if (radioMC.IsChecked)
            {
                filteredList = vehicleList
                    .Where(v => v.VehicleType == "MC")
                    .ToList();
            }
            else if (radioTruck.IsChecked)
            {
                filteredList = vehicleList
                    .Where(v => v.VehicleType == "Lastbil")
                    .ToList();
            }
            else
            {
                // Om "Alla" är vald, visa hela listan
                filteredList = vehicleList;
            }

            listViewVehicles.ItemsSource = filteredList;
        }

        private void OnSearchClicked(object sender, EventArgs e)
        {
            string searchTerm = entrySearchRegistrationNumber.Text?.ToLower();

            if (string.IsNullOrEmpty(searchTerm))
            {
                entrySearchRegistrationNumber.Text = "Ange ett registreringsnummer för att söka.";
                return;
            }

            var foundVehicle = vehicleList.FirstOrDefault(v => v.RegistrationNumber?.ToLower() == searchTerm);

            if (foundVehicle != null)
            {
                labelSearchResult.Text = $"Fordon hittat:\n" +
                                         $"Registreringsnummer: {foundVehicle.RegistrationNumber}\n" +
                                         $"Tillverkare: {foundVehicle.Manufacturer}\n" +
                                         $"Modell: {foundVehicle.Model}\n" +
                                         $"Typ: {foundVehicle.VehicleType}";
            }
            else
            {
                labelSearchResult.Text = "Inget fordon hittades med det registreringsnumret.";
            }
        }

        private void ClearTextFields()
        {
            entryRegistrationNumber.Text = string.Empty;
            entryManufacturer.Text = string.Empty;
            entryModel.Text = string.Empty;
        }
    }
}
