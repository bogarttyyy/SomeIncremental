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
    static readonly (string State, int Min, int Max)[] StatePostcodeRanges = {
        ("ACT", 1000, 1999),
        ("NSW", 2000, 2999),
        ("VIC", 3000, 3999),
        ("QLD", 4000, 4999),
        ("SA", 5000, 5999),
        ("WA", 6000, 6999),
        ("TAS", 7000, 7999),
        ("NT", 800, 999) // will zero-pad to 4 digits (e.g., 0800)
    };

    // Generates: Unit/Building Street Suburb, State Postcode (e.g., "101/3 Peter Rd, Northest, NSW 2000")
    public static string GetRandomAddress(Random rng = null, bool allowUnit = true)
    {
        rng ??= new Random();

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
        return $"{unitPrefix}{streetNumber} {streetName} {streetType}, {suburb}, {state} {postcode:D4}";
    }
}
