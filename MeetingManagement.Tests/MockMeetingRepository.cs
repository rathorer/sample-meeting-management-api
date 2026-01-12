using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;

namespace MeetingManagement.Test
{
    public class MockMeetingRepository : IMeetingRepository
    {
        public int CreateMeeting(Meeting meeting)
        {
            return 1;
        }

        public bool DeleteMeeting(int id)
        {
            return true;
        }

        public Meeting GetMeetingById(int id)
        {
            return new Meeting
            {
                Id = id,
                Title = "Test",
                Description = "Test",
                StartTime = DateTime.Now,
                EndTime = DateTime.Now.AddHours(1),
                Attendees = ["abc", "xyz"]
            };
        }

        public IList<Meeting> GetMeetings()
        {
            return new List<Meeting> { GetMeetingById(1), GetMeetingById(2) };
        }

        public bool UpdateMeeting(Meeting meeting)
        {
            return true;
        }
    }
}
