public struct EAddressParts
{
    public string UnitPrefix;
    public int StreetNumber;
    public string StreetName;
    public string StreetType;
    public string Suburb;
    public string State;
    public int Postcode;

    public string StreetLine => $"{UnitPrefix}{StreetNumber} {StreetName} {StreetType}";
    public string SuburbStateLine => $"{Suburb}, {State} {Postcode:D4}";
}