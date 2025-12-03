using System;

public static class RandomAussieAddressGenerator
{
    static readonly string[] StreetNames = {
        "Peter", "Harbor", "Gumtree", "Eucalypt", "Southern Cross", "Coastline", "Bushland", "Outback",
        "Coral", "Wattle", "Kookaburra", "Billabong", "Sunrise", "Dune", "Quartz", "Ironbark", "Banksia"
    };

    static readonly string[] StreetTypes = { "St", "Rd", "Ave", "Blvd", "Pl", "Ct", "Dr", "Way" };

    static readonly string[] Suburbs = {
        "Northest", "Bayhaven", "Riverbend", "Seabourne", "Stonefield", "Redwater", "Hillcrest",
        "Pinehurst", "Brightview", "Greylea", "Creston", "Kingsford", "Westvale", "Mariner", "Fernvale"
    };

    // State-associated postcode bands to keep fiction roughly aligned with AU formatting.
    // Fictional-but-familiar state codes as a nod to AU regions.
    static readonly (string State, int Min, int Max)[] StatePostcodeRanges = {
        ("ACR", 1000, 1999),
        ("NSQ", 2000, 2999),
        ("VIX", 3000, 3999),
        ("QLF", 4000, 4999),
        ("SOA", 5000, 5999),
        ("VA", 6000, 6999),
        ("TAD", 7000, 7999),
        ("NTE", 800, 999) // will zero-pad to 4 digits (e.g., 0800)
    };

    struct AddressParts
    {
        public string UnitPrefix;
        public int StreetNumber;
        public string StreetName;
        public string StreetType;
        public string Suburb;
        public string State;
        public int Postcode;
    }

    static AddressParts CreateAddressParts(bool allowUnit, Random rng)
    {
        var streetName = StreetNames[rng.Next(StreetNames.Length)];
        var streetType = StreetTypes[rng.Next(StreetTypes.Length)];
        var suburb = Suburbs[rng.Next(Suburbs.Length)];

        var (state, minPostcode, maxPostcode) = StatePostcodeRanges[rng.Next(StatePostcodeRanges.Length)];
        var postcode = rng.Next(minPostcode, maxPostcode + 1);

        string unitPrefix = string.Empty;
        if (allowUnit && rng.NextDouble() < 0.55)
        {
            var unit = rng.Next(1, 250);
            var building = rng.Next(1, 20);
            unitPrefix = $"{unit}/{building} ";
        }

        var streetNumber = rng.Next(1, 399);

        return new AddressParts
        {
            UnitPrefix = unitPrefix,
            StreetNumber = streetNumber,
            StreetName = streetName,
            StreetType = streetType,
            Suburb = suburb,
            State = state,
            Postcode = postcode
        };
    }

    static string FormatStreet(AddressParts parts) => $"{parts.UnitPrefix}{parts.StreetNumber} {parts.StreetName} {parts.StreetType}";
    static string FormatSuburbState(AddressParts parts) => $"{parts.Suburb}, {parts.State} {parts.Postcode:D4}";

    // Generates: Unit/Building Street Suburb, State Postcode (e.g., "101/3 Peter Rd, Northest, NSQ 2000")
    public static string GetRandomAddress(bool allowUnit = true, Random rng = null)
    {
        rng ??= new Random();
        var parts = CreateAddressParts(allowUnit, rng);
        return $"{FormatStreet(parts)}, {FormatSuburbState(parts)}";
    }

    // Line 1: unit/building street (e.g., "101/3 Peter Rd" or "12 Harbor St")
    public static string GetStreet(bool allowUnit = true, Random rng = null)
    {
        rng ??= new Random();
        var parts = CreateAddressParts(allowUnit, rng);
        return FormatStreet(parts);
    }

    // Line 2: suburb, state postcode (e.g., "Northest, NSQ 2000")
    public static string GetSuburbState(Random rng = null)
    {
        rng ??= new Random();
        var parts = CreateAddressParts(allowUnit: false, rng);
        return FormatSuburbState(parts);
    }
}
