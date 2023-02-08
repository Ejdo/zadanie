namespace Server.Models;

public class Employee
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string BirthDate { get; set; }
    public string JoinDate { get; set; }
    public float? Salary { get; set; }
    public bool isActive { get; set; }
    public int PositionId { get; set; }
    public IList<EmployeePosition>? EmployeePositions { get; set; } = new List<EmployeePosition>();
}

