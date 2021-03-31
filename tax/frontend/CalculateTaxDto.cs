using System.Collections.Generic;

namespace frontend
{
    public class CalculateTaxDto
    {
        public CalculateTaxTaxableAddressDto CalculateTaxTaxableAddressDto { get; }
        public IEnumerable<CalculateTaxLineDto> CalculateTaxLinesDto { get; }
        public CalculateTaxShippingChargesDto CalculateTaxShippingChargesDto { get; }

        public CalculateTaxDto(
            CalculateTaxTaxableAddressDto calculateTaxTaxableAddressDto,
            IEnumerable<CalculateTaxLineDto> calculateTaxLinesDto,
            CalculateTaxShippingChargesDto calculateTaxShippingChargesDto)
        {
            CalculateTaxTaxableAddressDto = calculateTaxTaxableAddressDto;
            CalculateTaxLinesDto = calculateTaxLinesDto;
            CalculateTaxShippingChargesDto = calculateTaxShippingChargesDto;
        }
    }
}