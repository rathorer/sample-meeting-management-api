using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingTaskManagement.Controllers;

[ApiController]
[Route("[controller]")]
public class TaskManagementController : ControllerBase
{
    private readonly ITaskRepository _taskRepository;
    private readonly IMeetingRepository _meetingRepository;
    public TaskManagementController(ITaskRepository taskRepository, IMeetingRepository meetingRepository)
    {
        _taskRepository = taskRepository;
        _meetingRepository = meetingRepository;
    }

    [HttpGet("{meetingId}", Name = "GetAllTasksOfMeeting")]
    public async Task<ActionResult<IList<TaskItem>>> GetAllTasksOfMeeting(int meetingId)
    {
        try
        {
            AssosiatedMeeting(meetingId);
        }
        catch (ArgumentException ex)
        {
            Response.StatusCode = 404;
            return NotFound(ex.Message);
        }
        return Ok(_taskRepository.GetTasksByMeetingId(meetingId));
    }

    [HttpPost(Name = "CreateTask")]
    public async Task<ActionResult<int>> Post(TaskItem task)
    {
        var meetingId = task.MeetingId;
        Meeting meeting;
        try
        {
            meeting = AssosiatedMeeting(meetingId);
        }
        catch (ArgumentException ex)
        {
            Response.StatusCode = 404;
            return NotFound(ex.Message);
        }
        task.MeetingId = meeting.Id;// Ensure the task is associated with the correct meeting, this is redundant but safe
        _taskRepository.CreateTask(task);
        return Ok(task.Id);
    }

    [HttpGet("task/{id}", Name = "GetTask")]
    public async Task<ActionResult<TaskItem>> Get(int id)
    {
        return Ok(_taskRepository.GetTaskById(id));
    }

    [HttpPut(Name = "UpdateTask")]
    public async Task<ActionResult<bool>> Put(TaskItem task)
    {
        var updated = _taskRepository.UpdateTask(task);
        return Ok(updated);
    }

    [HttpDelete(Name = "DeleteTask")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        return Ok(_taskRepository.DeleteTask(id));
    }
    
    private Meeting AssosiatedMeeting(int meetingId)
    {
        if (meetingId <= 0)
        {
            throw new ArgumentException("Invalid MeetingId");
        } 
        var meeting = _meetingRepository.GetMeetingById(meetingId);
        if (meeting == null)
        {
            throw new ArgumentException("No Meeting found for the given MeetingId.");
        }
        return meeting;
    }
}