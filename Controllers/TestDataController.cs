using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace task_management.Controllers;

[ApiController]
[Route("[controller]")]
public class TestDataController : ControllerBase
{
    private static readonly string[] Attendees =
    [
        "Rahul R", "Pramod W", "Ankint B", "John T",
        "Sam D", "Rajesh K", "Suresh M", "Karthik A",
        "Aman T", "Vikram S"
    ];

    private static readonly string[] Meetings =
    [
        "Scrum Standup", "Consolidation Grooming", "Task Management Design Discussion",
        "Task Management Design Review", "Code Collaboration and Reusable Components",
        "Threat Model Review", "Consolidation Town Hall", "Major Issue War Room",
        "Task Management Code Reviews", "Task Management Spring Planning"
    ];
    private readonly IMeetingRepository _meetingRepository;
    private readonly ITaskRepository _taskRepository;

    public TestDataController(IMeetingRepository meetingRepository, ITaskRepository taskRepository)
    {
        _meetingRepository = meetingRepository;
        _taskRepository = taskRepository;
    }

    // [HttpPost(Name = "CreateMultipleMeetings")]
    // public IList<Meeting> CreateMultipleMeetings([FromBody] int meetingsCount)
    // {
    //     var meetings = CreateMeetings(meetingsCount);
    //     SaveMeetingsToDb(meetings);
    //     return meetings;
    // }

    [HttpGet("meetings/create/{numberOfMeetings}", Name = "GetCreateMeetings")]
    public IList<Meeting> GetCreateMeetings(int numberOfMeetings)
    {
        var meetings = CreateMeetings(numberOfMeetings);
        SaveMeetingsToDb(meetings);
        return meetings;
    }

    [HttpGet("tasks/create/{numberOfTasks}", Name = "GetCreateTasks")]
    public IList<TaskItem> GetCreateTasks(int numberOfTasks)
    {
        var tasks = CreateTasks(numberOfTasks);
        SaveTasksToDb(tasks);
        return tasks;
    }

    private IList<TaskItem> CreateTasks(int numberOfTasks)
    {
        var allMeetings = _meetingRepository.GetMeetings();
        var meetingIds = allMeetings.Select(m => m.Id).ToArray();
        var tasks = Enumerable.Range(1, numberOfTasks).Select(index =>
        {
            return new TaskItem
            {
                Id = index,
                Title = "Random Task " + index,
                Description = "Description for task " + index,
                DueDate = DateTime.Now.AddDays(Random.Shared.Next(1, 30)),
                Status = (MeetingTaskManagement.Models.TaskStatus)Random.Shared.Next(0, 3),
                MeetingId = meetingIds[Random.Shared.Next(meetingIds.Length)]
            };
        })
        .ToArray();

        return tasks;
    }
    private void SaveTasksToDb(IList<TaskItem> tasks)
    {
        if (tasks == null || tasks.Count == 0)
        {
            return;
        }
        foreach (var task in tasks)
        {
            _taskRepository.CreateTask(task);
        }
    }
    private IList<Meeting> CreateMeetings(int numberOfMeetings)
    {
        var meetings = Enumerable.Range(1, numberOfMeetings).Select(index =>
        {
            var meetingStart = DateTime.Now.AddDays(Random.Shared.Next(1, 30))
                .AddHours(Random.Shared.Next(1, 10));
            var meetingEnd = meetingStart.AddHours(1);
            return new Meeting
            {
                Id = index,
                Title = Meetings[Random.Shared.Next(Meetings.Length)],
                Description = "Description for meeting " + Meetings[Random.Shared.Next(Meetings.Length)],
                StartTime = meetingStart,
                EndTime = meetingEnd,
                Attendees =
                    Enumerable.Range(1, 5)
                    .Select(i => Attendees[Random.Shared.Next(Attendees.Length)]).ToList()
            };
        })
        .ToArray();

        return meetings;
    }
    private void SaveMeetingsToDb(IList<Meeting> meetings)
    {
        if (meetings == null || meetings.Count == 0)
        {
            return;
        }
        foreach (var meeting in meetings)
        {
            _meetingRepository.CreateMeeting(meeting);
        }
    }
}
