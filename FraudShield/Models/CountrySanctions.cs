using System;
using System.Collections.Generic;

namespace FraudShield.Models
{
    public class CountrySanctions
    {
        public List<string> SourceCountryCode { get; set; }
        public List<string> DestinationCountryCode { get; set; }
    }
}
