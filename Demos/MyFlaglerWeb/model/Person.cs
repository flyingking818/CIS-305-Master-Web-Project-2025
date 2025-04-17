using System;

public abstract class Person
{
    public string Name { get; set; }
    public string ID { get; set; }
    public string Email { get; set; }
    public byte[] ProfileImage { get; set; }
    public abstract string GetDetails();
}