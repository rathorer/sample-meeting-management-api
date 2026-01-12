using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingTaskManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class ReportController : ControllerBase
{
    private readonly IMeetingRepository _meetingRepository;
    private readonly ITaskRepository _taskRepository;
    public ReportController(IMeetingRepository meetingRepository, ITaskRepository taskRepository)
    {
        _meetingRepository = meetingRepository;
        _taskRepository = taskRepository;
    }

    //Below api is not there in assignment, created for testing purpose
    [HttpGet("meeting/{meetingId}", Name = "GetMeetingWithTasks")]
    public async Task<ActionResult<Report>> GetMeetingWithTasks(int meetingId)
    {
        var meeting = _meetingRepository.GetMeetingById(meetingId);
        if (meeting == null)
        {
            return NotFound("No meeting found with the given id");
        }
        var tasks = _taskRepository.GetTasksByMeetingId(meetingId);
        var report = new Report
        {
            Meeting = meeting,
            AssociatedTasks = tasks
        };
        return Ok(report);
    }

    /// <summary>
    /// Get meetings in a date range along with their associated tasks
    /// </summary>
    /// <param name="from">Start date for filtering meetings (format: yyyy-MM-dd)</param>
    /// <param name="to">End date for filtering meetings (format: yyyy-MM-dd)</param>
    /// <returns></returns>
    [HttpGet("daterange/{from}/{to}", Name = "GetAllMeetingsWithTasks")]
    public async Task<ActionResult<Report>> GetAllMeetingsWithTasks(DateTime? from = null, DateTime? to = null)
    {
        var allMeetings = _meetingRepository.GetMeetings();
        var meetings = allMeetings
            .Where(m => (!from.HasValue || m.StartTime >= from.Value) &&
                        (!to.HasValue || m.EndTime <= to.Value))
            .ToList();
        var report = meetings.Select(meeting => new Report
        {
            Meeting = meeting,
            AssociatedTasks = _taskRepository.GetTasksByMeetingId(meeting.Id)
        });
        return Ok(report);
    }
}