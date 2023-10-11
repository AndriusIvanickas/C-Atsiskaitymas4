namespace ConsoleApp6
{
    internal class Flat : RealEstate
    {
        public int aukstas
        { get; set; }
        public string remontas
        { get; set; }
        public Flat(int aukstas, string remontas)
        {
            this.aukstas = aukstas;
            this.remontas = remontas;
        }
    }
}
