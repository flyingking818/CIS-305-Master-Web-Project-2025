using System;

public class Staff : Person
{
    public string Position { get; set; }
    public string Division { get; set; }
    public bool IsAdministrative { get; set; }

    public override string GetDetails()
    {
        return $"{Name}: {Position} | {Division} | Administrative: {IsAdministrative}";
    }
}