

namespace DotNetAndCSharp.Zoo;
public  record Monkey
{
public string? Name { get; set; }  
    public int Age { get; init; }
    //public required string FavoriteFood { get; init; } = string.Empty;
    
    //can use init over set to enforce only in object initializer
    // public string? Name {get; init;}
  
}

public record Gorilla(string Name, int Age);