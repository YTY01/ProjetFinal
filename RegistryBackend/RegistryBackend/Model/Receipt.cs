namespace RegistryBackend.Model
{
    public class Receipt
    {
        public int Id { get; set; }
        public double Total { get; set; }
        public double SousTotal { get; set; }
        public double Tps { get; set; }
        public double Tvq { get; set; }
        public Cart Cart { get; set; }
        public Member? Member { get; set; }
    }
}
