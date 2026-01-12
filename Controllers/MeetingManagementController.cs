using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingTaskManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class MeetingManagementController : ControllerBase
{
    private readonly IMeetingRepository _meetingRepository;
    public MeetingManagementController(IMeetingRepository meetingRepository)
    {
        _meetingRepository = meetingRepository;
    }

    [HttpGet("all", Name = "GetAllMeetings")]
    public async Task<ActionResult<IList<Meeting>>> GetAllMeetings()
    {
        return Ok(_meetingRepository.GetMeetings());
    }

    [HttpPost(Name = "CreateMeeting")]
    public async Task<ActionResult<int>> Post(Meeting meeting)
    {
        //Validation: End time should be after start time
        if(meeting.EndTime <= meeting.StartTime)
        {
            return BadRequest("End time must be after start time");
        }
        _meetingRepository.CreateMeeting(meeting);
        return Ok(meeting.Id);
    }

    [HttpGet("{id}", Name = "GetMeeting")]
    public async Task<ActionResult<Meeting>> Get(int id)
    {
        var meeting = _meetingRepository.GetMeetingById(id);
        if (meeting == null)
        {
            return NotFound("No meeting found with the given id");
        }
        return Ok(meeting);
    }

    [HttpPut(Name = "UpdateMeeting")]
    public async Task<ActionResult<bool>> Put(Meeting meeting)
    {
        var meetingUpdated = _meetingRepository.UpdateMeeting(meeting);
        return Ok(meetingUpdated);
    }

    [HttpDelete(Name = "DeleteMeeting")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var meetingDeleted = _meetingRepository.DeleteMeeting(id);
        return Ok(meetingDeleted);
    }
}