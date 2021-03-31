using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ServiceReference;

namespace backend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaxCalculationController : ControllerBase
    {
        private readonly ILogger<TaxCalculationController> _logger;

        private readonly TaxServiceClient.EndpointConfiguration _taxEndpoint = TaxServiceClient.EndpointConfiguration.WSHttpBinding_ITaxService;

        public TaxCalculationController(ILogger<TaxCalculationController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<TaxCalculationResult> Get()
        {
            var taxResponseDto = new TaxResponseDto();
            var taxRequestClient = new TaxServiceClient(_taxEndpoint);
            try
            {
                var taxRequestDto = GetTaxRequestDto();
                taxResponseDto = await taxRequestClient.CalculateTaxAsync(taxRequestDto);
                //taxResponseDto = taxRequestClient.CalculateTax(taxRequestDto);
                taxRequestClient.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                taxRequestClient.Abort();
            }

            static decimal Func(decimal d, decimal d2) => d > 0m ? d2 : 0m;

            var stateTax =
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedStateTaxAmount, x.StateTaxRate)) +
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedSecondaryStateTaxAmount, x.SecondaryStateTaxRate));
            var cityTax =
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedCityTaxAmount, x.CityTaxRate)) +
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedSecondaryCityTaxAmount, x.SecondaryCityTaxRate));
            var countyTax =
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedCountyTaxAmount, x.CountyTaxRate)) +
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedSecondaryCountyTaxAmount, x.SecondaryCountyTaxRate));
            var districtTax =
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedDistrictTaxAmount, x.DistrictTaxRate));
            var countryTax =
                taxResponseDto.OrderLines.Max(x => Func(x.CalculatedCountryTaxAmount, x.CountryTaxRate));

            return new TaxCalculationResult {
                TaxRate = (stateTax + cityTax + countyTax + districtTax + countryTax),
                //TaxRate = (stateTax + cityTax + countyTax + districtTax + countryTax) / 100m,
                TotalTax = taxResponseDto.OrderLines.Sum(rate => rate.CalculatedTotalTaxAmount)
            };
        }

        private static TaxRequestDto GetTaxRequestDto()
        {
            var lines = new List<TaxRequestLineDto>();
            //var address = dto.CalculateTaxTaxableAddressDto;
            var now = DateTime.Now;

            // ReSharper disable once ConvertToLocalFunction
            Action<TaxRequestLineDto, decimal, string, string, string> build = (d, amt, pc, @in, mi) =>
            {
                d.BusinessLocationCode = "Main";
                d.ShipToCity = "Loveland";
                d.ShipToCountry = "US";
                d.ShipToPostalCode = "80537";
                d.ShipToState = "CO";
                d.InvoiceDate = now;
                d.FiscalDate = now;
                d.LineItemAmount = amt;
                d.ProductCode = pc;
                d.ItemNumber = @in;
                d.MiscellaneousInfo = mi;
            };

            //foreach (var lineDto in dto.CalculateTaxLinesDto)
            //{
                var line = new TaxRequestLineDto { Quantity = 1 };
                build(line, 10.10m, "15000", "15000", "Item");
                lines.Add(line);

            //}

                return new TaxRequestDto
                {
                    CompanyId = "STP",
                    DivisionCode = "DIV",
                    OrderLines = lines.ToArray()
                };

        }

    }
}
