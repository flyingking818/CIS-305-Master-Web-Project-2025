using System;

public class Professor : Person
{
    public string Department { get; set; }
    public string ResearchArea { get; set; }
    public bool IsTerminalDegree { get; set; }

    public override string GetDetails()
    {
        return $"{Name}: {Department} Department | Research: {ResearchArea} | Terminal Degree: {IsTerminalDegree}";
    }
}