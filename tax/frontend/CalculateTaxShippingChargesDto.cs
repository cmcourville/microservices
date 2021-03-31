namespace frontend
{
    public class CalculateTaxShippingChargesDto
    {
        public decimal Charges { get; }
        public string ShippingMethodId { get; }

        public CalculateTaxShippingChargesDto(
            decimal charges,
            int shippingMethodId)
        {
            Charges = charges;
            ShippingMethodId = shippingMethodId.ToString();
        }
    }
}