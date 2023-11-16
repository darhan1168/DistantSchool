namespace DistantSchool.Models;

public class Review
{
    public Student Student { get; set; }
    public double AverageGrade { get; set; }
    public Subject Subject { get; set; }
}