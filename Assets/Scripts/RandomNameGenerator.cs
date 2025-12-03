using System;

public static class RandomNameGenerator
{
    static readonly string[] FirstNames = {
        "Ava", "Liam", "Mia", "Noah", "Ella", "Ethan", "Zara", "Mason", "Isla", "Leo",
        "Aria", "Jack", "Ruby", "Hudson", "Chloe", "Finn", "Sienna", "Owen", "Harper", "Caleb",
        "Elodie", "Miles", "Nora", "Ivy", "Asher", "Luca", "Piper", "Theo", "Maren", "Silas",
        "Veda", "Arlo", "Juniper", "Remy", "Callum", "Esme", "Wyatt", "Naomi", "Sawyer", "Freya",
        "Jasper", "Lena", "Colt", "Adeline", "Rory", "Gideon", "Maeve", "Felix", "Dahlia", "Rowan",
        "Clara", "Briar", "Emmett", "Poppy", "Hugo", "Tessa", "Atlas", "Mila", "Reese", "Cleo"
    };

    static readonly string[] MiddleNames = {
        "Grace", "James", "Lee", "Rose", "Kai", "Skye", "Drew", "Quinn", "Jude", "Reid",
        "Blair", "Brooke", "Wren", "Shay", "Cole", "Faye", "Hayes", "Rhys", "Lane", "Shea"
    };

    static readonly string[] LastNames = {
        "Hart", "Kelley", "Archer", "Sawyer", "Lennox", "Mercer", "Hale", "Rowan", "Parker", "Ellis",
        "Sutton", "Monroe", "Bennett", "Dalton", "Greer", "Harlow", "Sinclair", "Tanner", "Wilder", "Langley",
        "Callahan", "Drake", "Finch", "Granger", "Hastings", "Irwin", "Jarvis", "Keaton", "Larkin", "Maddox",
        "Nolan", "Oakley", "Prescott", "Quincy", "Radford", "Sterling", "Thatcher", "Underwood", "Vaughn", "Winslow",
        "York", "Carver", "Denton", "Easton", "Fletcher", "Gaines", "Hendricks", "Iverson", "Jensen", "Kerrigan",
        "Lyle", "Montrose", "Newell", "Pryor", "Ridley", "Samuels", "Thorne", "Ulrich", "Vickers", "Whitman"
    };

    // Returns a full name: First Last, with a middle name about 10% of the time.
    public static string GetRandomName(Random rng = null)
    {
        rng ??= new Random();

        var first = FirstNames[rng.Next(FirstNames.Length)];
        var last = LastNames[rng.Next(LastNames.Length)];

        string middle = string.Empty;
        if (rng.NextDouble() < 0.10)
        {
            middle = MiddleNames[rng.Next(MiddleNames.Length)];
        }

        return string.IsNullOrEmpty(middle) ? $"{first} {last}" : $"{first} {middle} {last}";
    }
}
