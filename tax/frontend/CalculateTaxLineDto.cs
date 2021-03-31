namespace frontend
{
    public class CalculateTaxLineDto
    {
        public decimal LineItemTotal { get; }
        public string BaseId { get; }
        public string Psc { get; }
        public string ItemNumber { get; }
        public int Quantity { get; }
        public decimal AssemblyCharge { get; }

        public CalculateTaxLineDto(
            decimal lineItemTotal,
            string baseId,
            string psc,
            string itemNumber,
            int quantity,
            decimal assemblyCharge)
        {
            LineItemTotal = lineItemTotal;
            BaseId = baseId;
            Psc = psc;
            ItemNumber = itemNumber;
            Quantity = quantity;
            AssemblyCharge = assemblyCharge;
        }
    }
}
