namespace PRG_MAUI_Car_Register;
using System.Text.RegularExpressions;
class Vehicle
{
    // Medlemsvariabler
    public enum Type { Bil, MC, Lastbil };
    private Type vehicleType;
    private string registrationNumber = string.Empty;
    private string manufacturer = string.Empty;
    private string model = string.Empty;


    // Konstruktor (en metod med samma namn som klassen, som returnerar ett objekt)
    public Vehicle(Type vehicleType) // en konstruktor kan, men måste inte, ta parametrar
    {
        this.vehicleType = vehicleType;
    }

    // Get-Set för att hålla variablerna privata, och för att validera inkommande värden från UI (user interface, användargränssnittet)
    public string RegistrationNumber
    {
        get { return registrationNumber; }

        set
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Registreringsnummer kan inte vara tomt.");

            }

            if (value.Length == 6)
                {
                    for (int i = 0; i < 3; i++)
                    {
                        if (!char.IsLetter(value[i]))
                            throw new ArgumentException("Inkorrekt registreringsnummer: De första tre tecknen måste vara bokstäver.");

                    }
                    for (int i = 3; i < 6; i++)
                    {
                        if (i < 5)
                        {
                            if (!char.IsDigit(value[i]))
                                throw new ArgumentException("Inkorrekt registreringsnummer: Det fjärde och femte tecknet måste vara siffror.");
                        }
                        else
                        {
                            if (!char.IsDigit(value[i]) && !char.IsLetter(value[i]))
                                throw new ArgumentException("Inkorrekt registreringsnummer: Det sjätte tecknet måste vara en siffra eller en bokstav.");
                        }
                    }
                         registrationNumber = value.ToUpper();

            }
            

            else
            {
                throw new ArgumentException("Ett registreringsnummer måste bestå av exakt 6 tecken, med tre bokstäver följt av två siffror och en siffra eller bokstav.");
            }
           
        }
    }

    // Fordonstyp tas in från dropdown-menyn, och behöver därför inte valideras
    public Type VehicleType
    {
        get { return vehicleType; }
        set { this.vehicleType = value; }
    }

    //TODO Tillverkare ska valideras, sparas i objektet och visas i UI
    public string Model
    {
        get { return model; }
        set
        {
            if (String.IsNullOrWhiteSpace(value))
            {
                throw new ArgumentException("Modell måste anges.");
            }

               
                if (!Regex.IsMatch(value, @"^[A-Za-zÅÄÖåäö0-9\s\-]+$"))
                {
                    throw new ArgumentException(
                        "Modellen innehållar ogiltiga tecken.");
                }
                model = value;
            }
        
        }
    

    //TODO Modell ska valideras, sparas i objektet och visas i UI
    public string Manufacturer { get 

        { return manufacturer; } 
        set { 
            if (string.IsNullOrWhiteSpace(value)) 
            { 
                throw new ArgumentException("Märke måste anges."); 
            }
            value = value.Trim();
            bool letters = false;

            foreach (char c in value)
            {
                if (char.IsLetter(c))
                {
                    letters = true;
                }
                if (char.IsDigit(c) && !char.IsWhiteSpace(c))
                {
                    throw new ArgumentException("Märke få inte innehålla siffror.");
                }
            }
            if (!letters)
            {
                throw new ArgumentException("Märke måste bara innehålla bokstäver.");
            }
            manufacturer = value; } 
    
    }
    //TODO Lägg till möjligheten att spara realistisk årsmodell, validera, spara och visa i objektet och visas i UI. Tips: Regex.IsMatch()



    private int year;

    public int Year
    {
        get { return year; }
        set
        {
            if (value < 1895 || value > DateTime.Today.Year)
            {
                throw new ArgumentException(
                    $"Årsmodellen måste vara mellan 1895 och {DateTime.Today.Year}");
            }
            if (!Regex.IsMatch(value.ToString(), @"^[1-2][0-9][0-9][0-9]$"))

            { 
                throw new ArgumentException("Årsmodellen måste bestå av fyra siffror."); 
            }

            year = value;
        }
    }
    //TODO Modifiera overriden på ToString() så att allt visas som önskat i UIs listBox
    public override string ToString()
    {
        return $"{registrationNumber}\t{vehicleType}\t{manufacturer}\t{model}\t{year}";
    }

}

