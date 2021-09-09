using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Projektarbete_Spelregister
{
    class Program
    {
        public static Spel[] games = new Spel[1];
        static void Main(string[] args)
        {
            Ladda();

            MenyVal();

            Spara();
        }

        // Flervalsalternativ som hämtar olika metoder beroende på val från andvändaren
        public static void MenyVal()
        {
            int val = 0;

            //inmatning för menyvalet samt stoppar användaren för att välja annat tal än vad som erbjuds
            while ((val = PrintMenu()) != 0)
            {
                if (val == 1)
                {
                    Inmatning();
                }

                if (val == 2)
                {
                    SkrivUt(games);
                }

                if (val == 3)
                {
                    BubbleSort(games);
                }

                if (val == 4)
                {
                    SökSpel();
                }

                if (val == 5)
                {
                    games = TaBort(games);
                }
            }
        }

        // Skapande av en meny till användaren
        public static int PrintMenu()
        {
            Header("Meny");
            Console.WriteLine();
            Console.WriteLine("1. Registrera nytt spel");
            Console.WriteLine("2. Se registrerade spel");
            Console.WriteLine("3. Sortera efter betyg");
            Console.WriteLine("4. Sök efter spel");
            Console.WriteLine("5. Ta bort ett spel");
            Console.WriteLine("0. Avsluta applikation");
            Console.Write("Menyval: ");
            string menyval = Console.ReadLine();
            int val;

            // hindrar användarn från att skriva en bokstav
            while (!int.TryParse(menyval, out val))
            {
                Console.Write("Ange en siffra: ");
                menyval = Console.ReadLine();
            }
            Console.Clear();
            return val;
        }

        public static void Inmatning()
        {
            Spel nyttSpel = new Spel();

            games = LäggTillSpel(games);

            //Inmatning av nytt spel genomförs med input från andvändare
            Console.Clear();
            Header("Registrera ett spel");
            Console.WriteLine();
            Console.Write("Ange plattform: ");
            nyttSpel.plattform = Console.ReadLine();

            Console.WriteLine();

            Console.Write("Ange spelnamn: ");
            nyttSpel.spelnamn = Console.ReadLine();
            Console.WriteLine();

            Console.Write("Ange genre: ");
            nyttSpel.genre = Console.ReadLine();
            Console.WriteLine();

            int utÅr;
            Console.Write("Ange utgivningsår: ");
            // Felhantering som hindrar användaren att mata in ett värde över eller under det rekommenderade
            while (!int.TryParse(Console.ReadLine(), out utÅr) || utÅr < 1950 || utÅr > 2999)
            {
                Console.Write("Utgivningsåren är mellan 1950 och 2999: ");
            }
            nyttSpel.utÅr = utÅr;
            Console.WriteLine();

            int betyg;
            Console.Write("Ange betyg mellan 1-10: ");
            // Felhantering som hindrar användaren att mata in ett värde över eller under det rekommenderade 
            while (!int.TryParse(Console.ReadLine(), out betyg) || betyg < 1 || betyg > 10)
            {
                Console.Write("Betygskalan är mellan 1-10: ");
            }
            nyttSpel.betyg = betyg;
            Console.WriteLine();

            // räknare
            int j = 0;

            for (int i = 0; i < games.Length; i++)
            {
                // Kontrollerar så att det finns en tom plats i vektorn
                // om inte, lägg till en ny plats.
                if (games[i] != null || games.Length == i)
                {
                    // utökar vektorn med en plats
                    games = LäggTillSpel(games);
                    j++;
                    continue;
                }
                // Stoppar in inläst spel i vektorn
                games[j] = nyttSpel;
            }
        }

        //Metod som utökar vektorn 
        public static Spel[] LäggTillSpel(Spel[] g)
        {
            Spel[] temp = new Spel[g.Length + 1];

            for (int i = 0; i < g.Length; i++)
            {
                temp[i] = g[i];
            }
            return temp;
        }

        /// <summary>
        /// Denna metoden skapar en "brygga" till textfil för att 
        /// läsa in värden. Vi andvänder oss av tabulering (\t) 
        /// för att separera de olika värderna. För att kunna 
        /// läsa in så sparar vi värderna i en vektor som vi 
        /// kör split på för att ta bort tabuleringen innan vi 
        /// skapar ett nytt spel.
        /// </summary>
        public static void Ladda()
        {
            try
            {
                StreamReader infil = new StreamReader("spel.txt");
                string rad;
                while ((rad = infil.ReadLine()) != null)
                {
                    Spel m = new Spel();
                    string[] attribut = rad.Split('\t');
                    m.plattform = attribut[0];
                    m.spelnamn = attribut[1];
                    m.genre = attribut[2];
                    m.utÅr = int.Parse(attribut[3]);
                    m.betyg = int.Parse(attribut[4]);

                    // Skapar en räknare
                    int j = 0;

                    for (int i = 0; i < games.Length; i++)
                    {
                        if (games[i] != null)
                        {
                            games = LäggTillSpel(games);
                            j++; // ökar räknaren med 1 
                            continue;
                        }
                        // Använder räknaren för att lägga in spel
                        games[j] = m;
                    }
                }
                infil.Close();
                Header("Välkommen till", "Spelregistret");
                Console.WriteLine();
            }
            // Felmedelnade om fil inte hittas
            catch (FileNotFoundException)
            {
                Console.WriteLine("Filen hittades inte");

            }
        }

        public static void SkrivUt(Spel[] g)
        {
            Header("Lagrade spel");
            // om inga element finns eller vektorn är tom skrivs felmeddelande
            if (g == null || g.Length == 0)
            {
                Console.WriteLine("Inget spel finns registrerat");
            }
            else
            {
                //skriver ut innehållet i vektorn till andvändaren
                for (int i = 0; i < g.Length; i++)
                {
                    if (g[i] != null)
                    {
                        //Console.WriteLine("{0}\tPlattform: {1}\tSpelnamn: {2}\tGenre: {3}\tUtgivningsår: {4}\tBetyg: ", g[i].plattform, g[i].spelnamn, g[i].genre, g[i].utÅr, g[i].betyg);
                        Console.WriteLine();
                        //skapar ett jämnt mellanrum mellan alla attribut i konsollen
                        Console.WriteLine("Plattform: {0, -15}\tSpelnamn: {1, -15}\tGenre: {2, -15}\tUtgivningsår: {3, -15}\tBetyg: {4, -15}|",
                            g[i].plattform, g[i].spelnamn, g[i].genre, g[i].utÅr, g[i].betyg);
                        Console.WriteLine();
                    }

                }
            }
            Console.ReadLine();
        }

        public static void Spara()
        {
            try
            {
                // skriver in värden till en textfil
                StreamWriter utfil = new StreamWriter("spel.txt");
                for (int i = 0; i < games.Length; i++)
                {

                    if (games[i] != null)
                    {
                        utfil.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}\t ", games[i].plattform, games[i].spelnamn, games[i].genre, games[i].utÅr, games[i].betyg);
                    }

                }
                utfil.Close();
                Console.WriteLine("Filen har sparats");
            }
            // felmdedelande ifall inget sparas
            catch (FileNotFoundException)
            {
                Console.WriteLine("Filen sparades inte");
            }
        }

        //sorterar vektorn efter betyg högst till lägst via en bubbelsort
        public static void BubbleSort(Spel[] g)
        {
            bool osorterad = true;
            int end = g.Length - 1;
            while (osorterad)
            {
                osorterad = false;
                for (int j = 0; j < end; j++)
                {
                    if (g[j] != null && g[j + 1] != null)
                    {
                        if (g[j].betyg < g[j + 1].betyg)
                        {
                            SwapSpel(g, j, j + 1);
                            osorterad = true;
                        }
                    }

                }
                end--;
            }
            Console.Clear();
            Header("Sorterade spel");
            Console.WriteLine();
            SkrivUt(g);
        }

        //Swap
        public static void SwapSpel(Spel[] games, int i, int j)
        {
            Spel tmp = games[i];
            games[i] = games[j];
            games[j] = tmp;
        }

        public static void SökSpel()
        {
            Console.Clear();
            Header("Sök spel");
            Console.WriteLine();
            // andvändare skriver in namn för att söka efter spel
            Console.Write("Ange spelnamn: ");
            string searchPhrase = Console.ReadLine();
            Spel[] hittadeSpel = HämtaSpel(searchPhrase);
            SkrivUt(hittadeSpel);
        }

        public static Spel[] HämtaSpel(string spel)
        {
            Spel[] hittadeSpel = new Spel[1];
            int j = 0;

            for (int i = 0; i < games.Length; i++)
            {
                // Felmeddelande
                if (games[0] == null)
                {
                    Console.WriteLine("Listan är tom");
                    break;
                }

                //hämtar spelet som sökts från andvändaren
                if (games[i] != null)
                {
                    if (games[i].spelnamn.ToUpper().Equals(spel.ToUpper()))
                    {
                        if (hittadeSpel[hittadeSpel.Length - 1] != null)
                        {
                            hittadeSpel = LäggTillSpel(hittadeSpel);
                            j++;
                        }
                        hittadeSpel[j] = games[i];
                    }
                }
            }
            return hittadeSpel;
        }

        public static Spel[] TaBort(Spel[] g)
        {
            //ber andvändaren mata in val för radering samt ger output på valen
            Header("Radera spel");
            Console.WriteLine();
            for (int i = 0; i < g.Length; i++)
            {
                if (g[i] != null)
                {
                    Console.WriteLine("{0} {1}", i, g[i].spelnamn);
                }
            }
            //tar bort valt spel via input från andvändaren//tar bort valt spel via input från andvändaren
            try
            {
                Spel[] temp = new Spel[g.Length - 1];

                Console.WriteLine();
                Console.WriteLine("Välj det spel du vill ta bort");
                string s = Console.ReadLine();
                int input;

                //skriver ut felmedelande om felaktig input ges från andvändaren
                while (!int.TryParse(s, out input))
                {
                    Console.WriteLine("Fel inmatning");
                    Console.WriteLine("Välj det spel du vill ta bort");
                    s = Console.ReadLine();
                }

                Console.WriteLine();

                for (int i = 0; i < input; i++)
                {
                    if (g[i] != null)
                    {
                        temp[i] = g[i];
                    }
                }

                for (int i = input + 1; i < g.Length; i++)
                {
                    temp[i - 1] = g[i];
                }
                return temp;
            }
            catch
            {
                //fel meddelande om minskning av vektor görs när inget spel finns registrerat
                Console.WriteLine("Inget spel finns registrerat");
                return games;
            }
        }

        //En metod som skapar en textbox med valfri text innuti
        //Texten som ska skrivas ut inuti en textbox väljs när denna metoden kallas på i de andra metoderna
        //Genom att skriva texten inne i metodanroppet
        //Designen inom en rektangel samt färgen är dock konstant och bestäms i denna metod
        public static void Header(string titel, string subtitel = "", ConsoleColor color = ConsoleColor.White)
        {
            int windowWidth = 90 - 2;
            string titelInehåll = String.Format("║{0," + ((windowWidth / 2) + (titel.Length / 2)) + "}{1," + (windowWidth - (windowWidth / 2) - (titel.Length / 2) + 1) + "}", titel, "║");
            string subtitelInehåll = String.Format("║{0," + ((windowWidth / 2) + (subtitel.Length / 2)) + "}{1," + (windowWidth - (windowWidth / 2) - (subtitel.Length / 2) + 1) + "}", subtitel, "║");

            Console.WriteLine("╔════════════════════════════════════════════════════════════════════════════════════════╗");
            Console.WriteLine(titelInehåll);
            if (!string.IsNullOrEmpty(subtitel))
            {
                Console.WriteLine(subtitelInehåll);
            }
            Console.WriteLine("╚════════════════════════════════════════════════════════════════════════════════════════╝");
        }
    }
}

