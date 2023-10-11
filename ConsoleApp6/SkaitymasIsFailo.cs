using System.Collections.Generic;
using System.IO;

namespace ConsoleApp6
{
    internal class SkaitymasIsFailo
    {
        private string failoKelias;

        public SkaitymasIsFailo(string filePath)
        {
            this.failoKelias = filePath;
        }

        public List<RealEstate> SkaitytyRealEstates()
        {

            List<RealEstate> realEstates = new List<RealEstate>();

            using (StreamReader reader = new StreamReader(failoKelias))
            {
                string line;
                while ((line = reader.ReadLine()) != null)
                {
                    string[] data = line.Split(';');
                    if (data.Length >= 0)
                    {
                        string tipas = data[0];
                        string agenturosPavadinimas = data[1];
                        string adresas = data[2];
                        string mikrorajonas = data[3];
                        int pastatymoMetai = int.Parse(data[4]);
                        int plotas = int.Parse(data[5]);
                        int kambariuSkaicius = int.Parse(data[6]);

                        if (tipas == "Namas")
                        {
                            string sildymoBudas = data[7];
                            string garazas = data[8];
                            House house = new House(sildymoBudas, garazas)
                            {
                                AgenturosPavadinimas = agenturosPavadinimas,
                                Adresas = adresas,
                                Mikrorajonas = mikrorajonas,
                                PastatymoMetai = pastatymoMetai,
                                Plotas = plotas,
                                KambariuSkaicius = kambariuSkaicius
                            };
                            realEstates.Add(house);
                        }
                        else if (tipas == "Butas")
                        {
                            int aukstas = int.Parse(data[7]);
                            string remontas = data[8];
                            Flat flat = new Flat(aukstas, remontas)
                            {
                                AgenturosPavadinimas = agenturosPavadinimas,
                                Adresas = adresas,
                                Mikrorajonas = mikrorajonas,
                                PastatymoMetai = pastatymoMetai,
                                Plotas = plotas,
                                KambariuSkaicius = kambariuSkaicius
                            };
                            realEstates.Add(flat);
                        }
                    }
                }
            }
            return realEstates;
        }
    }
}
