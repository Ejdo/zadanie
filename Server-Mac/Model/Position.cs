using System.Text.Json.Serialization;

namespace Server.Models;

public class Position
{
    public int Id { get; set; }
    public string Name { get; set; }

    [JsonIgnore]
    public IList<EmployeePosition>? EmployeePositions { get; set; } = new List<EmployeePosition>();
 
}

