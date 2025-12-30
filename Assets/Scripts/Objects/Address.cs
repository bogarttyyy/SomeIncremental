using Enums;

public struct Address
{
    public string UnitPrefix;
    public int StreetNumber;
    public string StreetName;
    public string StreetType;
    public string Suburb;
    public EStampState State;
    public int Postcode;

    public string StreetLine => $"{UnitPrefix}{StreetNumber} {StreetName} {StreetType}";
    public string SuburbStateLine => $"{Suburb}, {State.ToString().ToUpperInvariant()} {Postcode:D4}";
    
    public string CompleteAddress => $"{StreetLine}\n{SuburbStateLine}";
}