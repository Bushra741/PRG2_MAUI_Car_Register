namespace PRG_MAUI_Car_Register.Model
{
    internal class Truck : Vehicle
    {
        private double loadCapacity;

        public Truck(
    string vehicleType,
    string registrationNumber,
    string manufacturer,
    string model,
    int year,
    double loadCapacity)
    :   base(vehicleType, registrationNumber, manufacturer, model, year)
        {
            LoadCapacity = loadCapacity;
        }
        public double LoadCapacity
        {
            get { return loadCapacity; }

            set { loadCapacity = value; }
        }
        public override string GetDescription()
        {
            return $"{base.VehicleType}\t{base.RegistrationNumber}\t{base.Manufacturer}\t{base.Model}\t{base.Year}\t{loadCapacity}";
        }
    }
}