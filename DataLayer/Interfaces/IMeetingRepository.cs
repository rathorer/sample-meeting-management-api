using MeetingTaskManagement.Models;

namespace MeetingTaskManagement.DataLayer.Interfaces
{
    public interface IMeetingRepository
    {
        int CreateMeeting(Meeting meeting);
        Meeting GetMeetingById(int id);
        IList<Meeting> GetMeetings();
        bool UpdateMeeting(Meeting meeting);
        bool DeleteMeeting(int id);
    }
}