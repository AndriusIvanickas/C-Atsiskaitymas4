using System;
using System.Collections.Generic;
using System.Linq;

namespace ConsoleApp6
{
    internal class Program
    {

        static void Main(string[] args)
        {
            const string Duomenys = "duomenys.txt";


            List<RealEstate> realEstates;
            SkaitymasIsFailo skaitymasIsFailo = new SkaitymasIsFailo(Duomenys);
            realEstates = skaitymasIsFailo.SkaitytyRealEstates();

            Console.WriteLine("--------------------------------------------------");
            Console.WriteLine("|1. Mikrorajonai su daugiausia parduotu objektu  |");
            Console.WriteLine("|2. Filtravimas pagal plota                      |");
            Console.WriteLine("|3. Paieska                                      |");
            Console.WriteLine("|4. Pagalba                                      |");
            Console.WriteLine("--------------------------------------------------");
            int pasirinkimas = int.Parse(Console.ReadLine());

            // Parduoti objektai
            if (pasirinkimas == 1)
            {
                Console.WriteLine("Nuo kelintu pastatymo metu ieskoti: ");
                int metai = int.Parse(Console.ReadLine());

                // Filtruoja pagal pastatymo metus ir tada sugrupuoja pagal mikrorajona
                Dictionary<string, int> namuSkaicius = realEstates
                .OfType<House>()
                .Where(house => house.PastatymoMetai >= metai)
                .GroupBy(house => house.Mikrorajonas)
                .ToDictionary(group => group.Key, group => group.Count());

                Dictionary<string, int> butuSkaicius = realEstates
                    .OfType<Flat>()
                    .Where(flat => flat.PastatymoMetai >= metai)
                    .GroupBy(flat => flat.Mikrorajonas)
                    .ToDictionary(group => group.Key, group => group.Count());


                MikrorajonaiSuDaugiausiaParduotaMetodas(namuSkaicius, butuSkaicius);
                Console.WriteLine();
            }
            // Pagal Plota
            if (pasirinkimas == 2)
            {
                Console.WriteLine("Koks minimalus plotas: ");
                int minimumPlotas = int.Parse(Console.ReadLine());
                Console.WriteLine("Koks didziausias plotas: ");
                int maximumPlotas = int.Parse(Console.ReadLine());
                Console.WriteLine("Koks maziausias kambariu kiekis:");
                int kambariuKiekis = int.Parse(Console.ReadLine());
                List<RealEstate> pagalPlota = PagalPlota(realEstates, minimumPlotas, maximumPlotas, kambariuKiekis);
                AtspausdintiPagalPlota(pagalPlota);
            }
            // Paieska
            if (pasirinkimas == 3)
            {
                Console.WriteLine("Kokiu sildymo budu filtruoti (Dujos/Elektra) : ");
                string sildymoBudas = Console.ReadLine();
                Console.WriteLine("Kokiu remonto tipu filtruoti (Darytas/Ne) : ");
                string remontas = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine();
                SpausdintiFiltruotus(realEstates, sildymoBudas, remontas);
            }
            if(pasirinkimas ==4)
            {
                Console.WriteLine("Programa sudare Andrius Ivanickas. ");
                Console.WriteLine("Grupe: PRM");
            }


            Console.ReadLine();
        }


        static void MikrorajonaiSuDaugiausiaParduotaMetodas(Dictionary<string, int> namuSkaicius, Dictionary<string, int> butuSkaicius)
        {
            Dictionary<string, int> sudeta = namuSkaicius
                .Concat(butuSkaicius)
                .GroupBy(pair => pair.Key)
                .ToDictionary(group => group.Key, group => group.Sum(pair => pair.Value));

            int daugiausiaPardavimu = sudeta.Max(pair => pair.Value);

            var mikrorajonaiSuDaugiausiaiParduota = sudeta
                .Where(pair => pair.Value == daugiausiaPardavimu)
                .ToList();

            if (mikrorajonaiSuDaugiausiaiParduota.Count > 0)
            {
                Console.WriteLine("Mikrorajonai su daugiausia parduotu butu ir namu: ");
                foreach (var mikrorajonas in mikrorajonaiSuDaugiausiaiParduota)
                {
                    Console.WriteLine($"Mikrorajonas: {mikrorajonas.Key}");
                    Console.WriteLine($"Parduotu kiekis: {mikrorajonas.Value}");
                }
            }
            else
            {
                Console.WriteLine("nera tokiu");
            }
        }


        static List<RealEstate> PagalPlota(List<RealEstate> realEstates, int minPlotas, int maxPlotas, int kambariuKiekis)
        {
            return realEstates.Where(a => a is House && a.Plotas >= minPlotas && a.Plotas <= maxPlotas && a.KambariuSkaicius >= kambariuKiekis).ToList();
        }

        static void AtspausdintiPagalPlota(List<RealEstate> realEstates)
        {
            List<RealEstate> rikiuoti = realEstates.OrderByDescending(estate => estate.Plotas).ThenBy(estate => estate.Tipas).ToList();
            foreach (RealEstate estate in rikiuoti)
            {

                Console.WriteLine($"{estate.AgenturosPavadinimas}, {estate.Adresas}, {estate.Plotas}");
            }

        }

        static void SpausdintiFiltruotus(List<RealEstate> realEstates, string sildymoBudas, string remontas)
        {
            List<House> filtrasSildymas = realEstates.OfType<House>().Where(h => h.SildymoBudas == sildymoBudas).OrderByDescending(estate => estate.Plotas).ToList();
            List<Flat> filtrasRemontas = realEstates.OfType<Flat>().Where(f => f.remontas == remontas).OrderByDescending(estate => estate.Plotas).ToList();
            foreach (House house in filtrasSildymas)
            {
                Console.WriteLine($"{house.AgenturosPavadinimas}, {house.Adresas}, {house.Mikrorajonas},{house.PastatymoMetai},{house.Plotas},{house.KambariuSkaicius},{house.SildymoBudas},{house.Garazas}");
            }
            foreach (Flat flat in filtrasRemontas)
            {
                Console.WriteLine($"{flat.AgenturosPavadinimas}, {flat.Adresas},{flat.Mikrorajonas},{flat.PastatymoMetai}, {flat.Plotas},{flat.KambariuSkaicius},{flat.aukstas},{flat.remontas}");
            }
        }

    }
}
