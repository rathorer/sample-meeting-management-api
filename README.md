Meeting and task managemement APIs samples

## VS Code setup:
1. Clone the repo from https://github.com/rathorer/sample-meeting-management-api
2. From VS Code terminal run: 
    dotnet build task-management.sln
3. Run:
    dotnet run
4. Go to browser and visit http://localhost:{port}/swagger/index.html
 for most cases port is 5243 from vs code.
## There are 4 APIs:
1. meetingmanagement 
    a. to get all meetings: can be called as http://localhost:{port}/meetingmanagement/all
    b. to create meeting:
2. taskmanagement 
    a. (can be called as http://localhost:5243/taskmanagement/task/1)
    b. To get tasks of a meeting use http://localhost:5243/taskmanagement/1
3. report 
    a. Takes date range to fetch meetings and its tasks
4. The 4th Api for TestData is just to help testing:
    a. Create sample meetings using:  testdata/meetings/create/{number}
    b. Or create tasks to associate with meetings: testdata/tasks/create/{number}

Swagger documentation of APIs at:
http://localhost:{port}/swagger/index.html
