using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;

namespace MeetingTaskManagement.DataLayer
{
    public class MeetingRepository : IMeetingRepository
    {
        private readonly InMemoryDbContext _dbContext;
        public IQueryable<Meeting> Meetings => _dbContext.Meetings;
        public IQueryable<TaskItem> Tasks => _dbContext.Tasks;

        public MeetingRepository(InMemoryDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public int CreateMeeting(Meeting meeting)
        {
            _dbContext.Meetings.Add(meeting);
            _dbContext.SaveChanges();
            return meeting.Id;
        }

        public Meeting GetMeetingById(int id)
        {
            var meeting = _dbContext.Meetings.Find(id);
            return meeting;;
        }

        public IList<Meeting> GetMeetings()
        {
            return _dbContext.Meetings.ToList();
        }

        public bool UpdateMeeting(Meeting meeting)
        {
            var existingMeeting = _dbContext.Meetings.Find(meeting.Id);
            if (existingMeeting == null)
            {
                return false;
            }
            _dbContext.Meetings.Update(meeting);
            _dbContext.SaveChanges();
            return true;
        }

        public bool DeleteMeeting(int id)
        {
            var meeting = _dbContext.Meetings.Find(id);
            if (meeting == null)
            {
                return false;
            }
            _dbContext.Meetings.Remove(meeting);
            _dbContext.SaveChanges();
            return true;
        }
    }
}