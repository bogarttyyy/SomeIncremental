public struct Name
{
    public string lastName;
    public string firstName;
    public string middleName;
    
    public string FullName =>  string.IsNullOrEmpty(middleName) ? $"{firstName} {lastName}" : $"{firstName} {middleName} {lastName}";
}
