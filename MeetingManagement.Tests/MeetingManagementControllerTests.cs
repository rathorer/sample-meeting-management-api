using MeetingManagement.Test;
using MeetingTaskManagement.DataLayer.Interfaces;
using MeetingTaskManagement.Models;
using Microsoft.AspNetCore.Mvc;

namespace MeetingManagement.Tests
{
    [TestClass]
    public sealed class MeetingManagementControllerTests
    {

        private const string ValidStartDate = "2023-10-01T10:00:00Z";
        private const string ValidEndDate = "2023-10-01T11:00:00Z";
        private IMeetingRepository _meetingRepository;
        public MeetingManagementControllerTests()
        {
            _meetingRepository = new MockMeetingRepository();
        }

        [TestMethod]
        public void CreateMeetingTest_ValidStartAndEndDate()
        {
            var controller = new MeetingManagementController(_meetingRepository);
            var meetingStart = DateTime.Now;
            var meetingEnd = meetingStart;
            var meeting = new Meeting
            {
                Title = "Project Kickoff",
                Description = "Initial project kickoff meeting",
                StartTime = meetingStart,
                EndTime = meetingEnd,
                Attendees = new List<string> { "abc", "xyz"}
            };
            var response = controller.Create(meeting);

            var badRequestResult = (BadRequestObjectResult)response.Result;
            Assert.IsNotNull(badRequestResult.Value);
            //Result should be bad request due to start end date being the same
            Assert.AreEqual(400, badRequestResult.StatusCode);
        }

        [TestMethod]
        public void GetMeetingTest_ValidMeetingId()
        {
            var controller = new MeetingManagementController(_meetingRepository);
            var meetingStart = DateTime.Now;
            var meetingEnd = meetingStart;
            var meeting = new Meeting
            {
                Id = 1,
                Title = "Project Kickoff",
                Description = "Initial project kickoff meeting",
                StartTime = meetingStart,
                EndTime = meetingEnd,
                Attendees = new List<string> { "abc", "xyz"}
            };
            var result = controller.Create(meeting);
            var getResponse = controller.Get(1);

            var okResult = (OkObjectResult)getResponse.Result;
            Assert.IsNotNull(okResult.Value);
            Assert.AreEqual(200, okResult.StatusCode);
        }

        [TestMethod]
        public void GetMeetingTest_InvalidMeetingId()
        {
            var controller = new MeetingManagementController(_meetingRepository);
            var meetingStart = DateTime.Now;
            var meetingEnd = meetingStart;
            var meeting = new Meeting
            {
                Id = 1,
                Title = "Project Kickoff",
                Description = "Initial project kickoff meeting",
                StartTime = meetingStart,
                EndTime = meetingEnd,
                Attendees = new List<string> { "abc", "xyz"}
            };
            var result = controller.Create(meeting);
            var getResponse = controller.Get(2);

            var notFoundResult = (NotFoundObjectResult)getResponse.Result;
            Assert.IsNotNull(notFoundResult.Value);
            Assert.AreEqual(404, notFoundResult.StatusCode);
        }
    }
}
