namespace frontend
{
    public class CalculateTaxTaxableAddressDto
    {
        public string Street1 { get; }
        public string Street2 { get; }
        public string City { get; }
        public string State { get; }
        public string ZipCode { get; }
        public string Country { get; }
        public bool IsUnitedStates { get; }
        public string CountryCodeForOms { get; }

        public CalculateTaxTaxableAddressDto(
            string street1,
            string street2,
            string city,
            string state,
            string zipCode,
            string country,
            bool isUnitedStates,
            string countryCodeForOms)
            //Maybe<ZipPlus4Service> zipPlus4Service)
        {
            Street1 = street1;
            Street2 = street2;
            City = city;
            State = state;
            ZipCode = zipCode;
            Country = country;
            IsUnitedStates = isUnitedStates;
            CountryCodeForOms = countryCodeForOms;

            //if (zipPlus4Service.HasNoValue) return;
            //var plus4Address = new Address(street1, street1, city, state, zipCode, country);
            //var plus4Result = zipPlus4Service.Value.Get(plus4Address);
            //if (plus4Result.HasNoValue) return;
            //ZipCode = $"{zipCode}-{plus4Result.Value}";
        }
    }
}