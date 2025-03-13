using TaskTrackerCLI.Service;
using TaskTrackerCLI.Service.Interface;
using Task = TaskTrackerCLI.Models.Task;

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
        Task model = new Task();
        Console.WriteLine("Create description for a task");
        Description : string description = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(description))
        {
            Console.WriteLine("Please enter a description");
            goto Description;
        }
        model.Description = description;
        _taskService.Create(model);
        Console.WriteLine("Task successfully created");
    }

    public void Update()
    {
       Id: Console.WriteLine("Enter Id of task to update");
        string text = Console.ReadLine();
        int id;
        bool answer= int.TryParse(text, out id);
        if (answer)
        {
            var  task  =  _taskService.GetById(id);
            if (task is null)
            {
                Console.WriteLine("Task not found");
                goto Id;
            }
            Console.WriteLine("Enter new Description");
            var description = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(description))
            {
                task.Description = description;
            }

            Status:Console.WriteLine("Choose status:\n1:In progress\n2:Completed\n3:Todo");
            int status = int.Parse(Console.ReadLine());
            switch (status)
            {
                case 1:
                    task.Status = "In-Progress";
                    break;
                case 2:
                    task.Status = "Completed";
                    break;
                case 3:
                    task.Status = "Todo";
                    break;
                default:
                    Console.WriteLine("Please enter a valid status");
                    goto Status;
            }
            _taskService.Update(id,task);
            Console.WriteLine("Task successfully updated");
            return;
        }
        Console.WriteLine("Only numbers are allowed");
        goto Id;
        
        
    }

    public void ListAllTasks()
    {
        var tasks = _taskService.GetAll();
        if (tasks is null)
        {
            Console.WriteLine("No tasks found");
        }
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID:{task.Id} \n Description:{task.Description} \n Status:{task.Status} \n CreatedAt:{task.CreatedAt}");
        }
    }
}