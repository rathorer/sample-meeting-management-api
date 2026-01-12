using MeetingTaskManagement.Models;

namespace MeetingTaskManagement.DataLayer.Interfaces
{
    public interface ITaskRepository
    {
        int CreateTask(TaskItem task);
        TaskItem GetTaskById(int id);
        IList<TaskItem> GetTasksByMeetingId(int meetingId);
        bool UpdateTask(TaskItem task);
        bool DeleteTask(int id);
    }
}