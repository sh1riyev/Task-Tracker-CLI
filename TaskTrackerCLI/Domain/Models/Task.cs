namespace TaskTrackerCLI.Models;

public class Task : BaseEntity
{
    public string Description { get; set; }
    public string Status { get; set; }
}