using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;

namespace MeetingTaskManagement.DataLayer
{
    public class TaskRepository : ITaskRepository
    {
        private readonly InMemoryDbContext _dbContext;
        public IQueryable<TaskItem> Tasks => _dbContext.Tasks;
        public IQueryable<Meeting> Meetings => _dbContext.Meetings;

        public TaskRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CreateTask(TaskItem task)
        {
            _dbContext.Tasks.Add(task);
            _dbContext.SaveChanges();
            return task.Id;
        }

        public TaskItem GetTaskById(int id)
        {
            var task = _dbContext.Tasks.Find(id);
            return task;
        }

        public IList<TaskItem> GetTasksByMeetingId(int meetingId)
        {
            return _dbContext.Tasks.Where(t => t.MeetingId == meetingId).ToList();
        }

        public bool UpdateTask(TaskItem task)
        {
            var existingTask = _dbContext.Tasks.Find(task.Id);
            if (existingTask == null)
            {
                return false;
            }
            _dbContext.Tasks.Update(task);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteTask(int id)
        {
            var task = _dbContext.Tasks.Find(id);
            if (task == null)
            {
                return false;
            }
            _dbContext.Tasks.Remove(task);
            _dbContext.SaveChanges();
            return true;
        }
    }
}