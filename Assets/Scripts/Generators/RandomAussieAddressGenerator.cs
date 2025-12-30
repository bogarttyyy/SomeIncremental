using System;
using Enums;

public static class RandomAussieAddressGenerator
{
    public static event Action AddressGenerated;
    
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
    static readonly (EStampState State, int Min, int Max)[] StatePostcodeRanges = {
        (EStampState.Acr, 1000, 1999),
        (EStampState.Nsq, 2000, 2999),
        (EStampState.Vik, 3000, 3999),
        (EStampState.Qlt, 4000, 4999),
        (EStampState.Se, 5000, 5999),
        (EStampState.Va, 6000, 6999),
        (EStampState.Taz, 7000, 7999),
        (EStampState.Nd, 800, 999) // will zero-pad to 4 digits (e.g., 0800)
    };

    public static Address CreateAddressParts(bool allowUnit = true, Random rng = null)
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
        
        AddressGenerated?.Invoke();

        return new Address
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

    // Generates: Unit/Building Street Suburb, State Postcode (e.g., "101/3 Peter Rd, Northest, NSQ 2000")
    public static string GetRandomAddress(bool allowUnit = true, Random rng = null)
    {
        var parts = CreateAddressParts(allowUnit, rng);
        return $"{parts.StreetLine}, {parts.SuburbStateLine}";
    }

    // Line 1: unit/building street (e.g., "101/3 Peter Rd" or "12 Harbor St")
    public static string GetStreet(Address parts)
    {
        return parts.StreetLine;
    }

    // Line 2: suburb, state postcode (e.g., "Northest, NSQ 2000")
    public static string GetSuburbState(Address parts)
    {
        return parts.SuburbStateLine;
    }
}
