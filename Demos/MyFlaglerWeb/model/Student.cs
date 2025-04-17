using System;

public class Student : Person
{
    public string Major { get; set; }
    public double GPA { get; set; }
    public bool IsFullTime { get; set; }
    public DateTime EnrollmentDate { get; set; }

    public override string GetDetails()
    {
        return $"{Name}: {Major} Major | GPA: {GPA} | Enrollment Date: {EnrollmentDate.ToString()} | Full-Time: {IsFullTime}";
    }
}