using System;

public static class RandomNameGenerator
{
    static readonly string[] FirstNames = {
        "Amina", "Mateo", "Priya", "Wei", "Leila", "Yusuf", "Hana", "Malik", "Sofia", "Diego",
        "Mei", "Arjun", "Samira", "Luca", "Nia", "Omar", "Keiko", "Rafael", "Zahra", "Elias",
        "Carmen", "Hiro", "Ayana", "Jonas", "Anika", "Tariq", "Ayesha", "Jin", "Fatima", "Kofi",
        "Soren", "Amara", "Chen", "Imani", "Rina", "Naveen", "Marisol", "Dario", "Zaid", "Priyanka",
        "Kenji", "Farah", "Emilio", "Anish", "Soraya", "Idris", "Yara", "Pavel", "Nikhil", "Sana",
        "Dmitri", "Thandi", "Bashir", "Jae", "Ayoub", "Mireya", "Haruto", "Tenzin", "Sahar", "Kwame", "Charo"
    };

    static readonly string[] MiddleNames = {
        "Grace", "James", "Lee", "Rose", "Kai", "Skye", "Drew", "Quinn", "Jude", "Reid",
        "Blair", "Brooke", "Wren", "Shay", "Cole", "Faye", "Hayes", "Rhys", "Lane", "Shea", "May"
    };

    static readonly string[] LastNames = {
        "Ahmed", "Martinez", "Nguyen", "Patel", "Haddad", "Okafor", "Chen", "Ivanov", "Kim", "Singh",
        "Garcia", "Diallo", "Mori", "Khan", "Petrova", "Mensah", "Rahman", "Costa", "Kawamura", "Hussein",
        "Alvarez", "Zhang", "Mbatha", "Lopez", "Yilmaz", "Das", "Silva", "Bello", "Torres", "Rossi",
        "Baba", "Leblanc", "Abadines", "Tanaka", "Osei", "Volkov", "Saidi", "Popescu", "Chandra", "Mendez",
        "DeSouza", "Bekele", "Hashimoto", "Romero", "Mwangi", "Ghosh", "Nakamura", "Conteh", "Fernandes", "Abiola",
        "Karim", "Sato", "Noorani", "Kowalski", "Batista", "Rojas", "Onwu", "Esquivel", "Petrovic", "Delgado"
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
