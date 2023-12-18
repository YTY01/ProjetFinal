namespace RegistryBackend.Model
{
    public class ReceiptDTO : Receipt
    {
        public CartDTO Cart { get; set; }
        public double? MemberDiscount { get; set; }

        public ReceiptDTO(Receipt receipt)
        {
            SousTotal = receipt.SousTotal;
            Total = receipt.Total;
            Tps = receipt.Tps;
            Tvq = receipt.Tvq;
            Member = receipt.Member;
            Id = receipt.Id;
        }
    }
}
