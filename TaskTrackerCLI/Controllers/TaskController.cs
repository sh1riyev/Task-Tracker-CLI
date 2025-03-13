using TaskTrackerCLI.Service;
using TaskTrackerCLI.Service.Interface;

namespace TaskTrackerCLI.Controllers;

public class TaskController
{
    private readonly ITaskService _taskService;

    public TaskController()
    {
        _taskService = new TaskService();
    }

    public void Create()
    {
        
    }
}