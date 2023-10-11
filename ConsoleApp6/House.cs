namespace ConsoleApp6
{
    internal class House : RealEstate
    {
        public string SildymoBudas
        { get; set; }
        public string Garazas
        { get; set; }
        public House(string SildymoBudas, string Garazas)
        {
            this.SildymoBudas = SildymoBudas;
            this.Garazas = Garazas;
        }
    }
}
