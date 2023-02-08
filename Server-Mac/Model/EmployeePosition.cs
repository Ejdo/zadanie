using System.Text.Json.Serialization;

namespace Server.Models;

public class EmployeePosition
{
    public int Id { get; set; }
    public string startDate { get; set; }
    public string? endDate { get; set; }

    [JsonIgnore]
    public Employee? Employee { get; set; }
    public Position? Position { get; set; }

}