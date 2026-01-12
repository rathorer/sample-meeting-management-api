namespace MeetingTaskManagement.Models;
public class Report
{
    public Meeting Meeting { get; set; }
    public IList<TaskItem> AssociatedTasks { get; set; }
}