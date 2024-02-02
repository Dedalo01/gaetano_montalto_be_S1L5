namespace progetto_settimanale_be_S1L5
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            const string HEADING = "$$$$$$$\\                               \r\n$$  __$$\\                              \r\n$$ |  $$ |$$$$$$\\   $$$$$$\\   $$$$$$\\  \r\n$$$$$$$  |\\____$$\\ $$  __$$\\  \\____$$\\ \r\n$$  ____/ $$$$$$$ |$$ /  $$ | $$$$$$$ |\r\n$$ |     $$  __$$ |$$ |  $$ |$$  __$$ |\r\n$$ |     \\$$$$$$$ |\\$$$$$$$ |\\$$$$$$$ |\r\n\\__|      \\_______| \\____$$ | \\_______|\r\n                   $$\\   $$ |          \r\n                   \\$$$$$$  |          \r\n                    \\______/";

            do
            {
                Console.WriteLine(HEADING);
                Console.WriteLine();

                Console.WriteLine("Benvenuto,");
                Console.WriteLine("1. Immetti dati per calcolo delle imposte.");
                Console.WriteLine("2. Esci dal programma.");
                Console.WriteLine();
                Console.Write("Inserisci 1 o 2 per proseguire: ");

                byte selectedOption = byte.Parse(Console.ReadLine());
                switch (selectedOption)
                {

                    case 1:

                        Console.WriteLine("Immetti i dati qui sotto per calcolare le tue imposte");
                        Console.WriteLine();

                        Console.Write("Inserisci nome: ");
                        string? name = Console.ReadLine();

                        Console.Write("Inserisci cognome: ");
                        string surname = Console.ReadLine();

                        Console.Write("Inserisci giorno di nascita: ");
                        byte day = byte.Parse(Console.ReadLine());

                        Console.Write("Inserisci mese di nascita: ");
                        byte month = byte.Parse(Console.ReadLine());

                        Console.Write("Inserisci anno di nascita: ");
                        int year = int.Parse(Console.ReadLine());

                        DateTime birthday = new DateTime(year, month, day);

                        Console.Write("Inserisci codice fiscale (16 caratteri): ");
                        string fiscalCode = Console.ReadLine();

                        Console.Write("Inserisci sesso: ");
                        char sex = char.Parse(Console.ReadLine().ToUpper());

                        Console.Write("Inserisci città di residenza: ");
                        string cityOfResidence = Console.ReadLine();

                        Console.Write("Inserisci importo annuo: ");
                        double annualIncome = double.Parse(Console.ReadLine());
                        Console.WriteLine();

                        try
                        {
                            Contribuente user = new Contribuente(name, surname, birthday, fiscalCode, sex, cityOfResidence, annualIncome);
                            user.DisplayReport();
                        }
                        catch (ArgumentException ex)
                        {
                            Console.WriteLine($"Errore: {ex.Message}");
                        }
                        catch
                        {
                            Console.WriteLine("Errore: Non puoi lasciare campi vuoti.");
                        }
                        break;

                    case 2:
                        Console.WriteLine("Grazie per aver usato il nostro calcoatore di imposte. Arrivederci!");
                        Console.WriteLine();
                        Console.Write("Premi qualunque tasto per uscire... ");
                        Console.ReadKey();
                        return;

                    default:
                        Console.WriteLine("Opzione non valida, inserisci 1 o 2.");
                        break;
                }


                Console.WriteLine();
                Console.Write("Prema qualunque tasto per tornare al menù iniziale... ");
                Console.ReadKey();
                Console.Clear();
            } while (true);

        }

        class Contribuente
        {
            string _name;
            string _surname;
            DateTime _birthday;
            string _fiscalCode;
            char _sex;
            string _cityOfResidence;
            double _annualIncome;
            double _taxToBePaid;

            string Name
            {
                get { return _name; }
                set
                {
                    if (value == null && value.Length == 0)
                    {
                        throw new ArgumentException("Name cannot be Empty.");
                    }
                    _name = ToUpperFirstLetter(value);
                }
            }

            string Surname
            {
                get { return _surname; }
                set
                {
                    if (value.Length == 0)
                    {
                        throw new ArgumentException("Surname cannot be Empty.");
                    }
                    _surname = ToUpperFirstLetter(value);
                }
            }

            string FiscalCode
            {

                get { return _fiscalCode; }
                set
                {
                    if (value.Length == 0 || value.Length > 16) throw new ArgumentException("Fiscal Code must have 16 characters.");
                    _fiscalCode = value.ToUpper();
                }
            }

            char Sex
            {
                get { return _sex; }
                set
                {
                    if (value.Equals('M') || value.Equals('F'))
                    {
                        _sex = value;
                    }
                    else
                    {
                        throw new ArgumentException("Biological Sex must be [M]ale or [F]emale.");
                    }

                }
            }

            string CityOfResidence
            {
                get { return _cityOfResidence; }
                set
                {
                    if (value.Length == 0)
                    {
                        throw new ArgumentException("City of Residence cannot be Empty.");
                    }
                    _cityOfResidence = ToUpperFirstLetter(value);
                }
            }

            int AnnualIncome { get; }

            public Contribuente(string name, string surname, DateTime birthday, string fiscalCode, char sex, string cityOfResidence, double annualIncome)
            {
                Name = name;
                Surname = surname;
                _birthday = birthday;
                FiscalCode = fiscalCode;
                Sex = sex;
                CityOfResidence = cityOfResidence;
                _annualIncome = annualIncome;

                ApplyRate(annualIncome);

            }

            string ToUpperFirstLetter(string val)
            {
                return char.ToUpper(val[0]) + val.Substring(1).ToLower();
            }

            void ApplyRate(double annualIncome)
            {
                if (annualIncome >= 75001)
                {
                    _taxToBePaid = 25420 + ((annualIncome - 75000) * 0.43);
                }
                else if (annualIncome >= 55001)
                {
                    _taxToBePaid = 17220 + ((annualIncome - 55000) * 0.41);
                }
                else if (annualIncome >= 28001)
                {
                    _taxToBePaid = 6960 + ((annualIncome - 28000) * 0.38);
                }
                else if (annualIncome >= 15001)
                {
                    _taxToBePaid = 3450 + ((annualIncome - 15000) * 0.27);
                }
                else
                {
                    _taxToBePaid = annualIncome * 0.23;
                }
            }

            public void DisplayReport()
            {
                Console.WriteLine("\\==========================================/");
                Console.WriteLine();
                Console.WriteLine("CALCOLO DELL'IMPOSTA DA VERSARE:");
                Console.WriteLine();
                Console.WriteLine($"Contribuente: {Name} {Surname},");
                Console.WriteLine($"nato il {_birthday.ToShortDateString()} ({Sex}),");
                Console.WriteLine($"residente in {CityOfResidence},");
                Console.WriteLine($"codice fiscale: {FiscalCode}");
                Console.WriteLine($"Reddito dichiarato: {AnnualIncome:C}");
                Console.WriteLine($"IMPOSTA DA VERSARE: {_taxToBePaid:C}");
                Console.WriteLine();
                Console.WriteLine("/==========================================\\");
            }
        }
    }
}
