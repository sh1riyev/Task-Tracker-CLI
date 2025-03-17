using TaskTrackerCLI.Service;
using TaskTrackerCLI.Service.Helpers.ExceptionHandler;
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
    public void Delete()
    {
        Id: Console.WriteLine("Enter Id of task to delete");
        string text = Console.ReadLine();
        int id;
        bool answer= int.TryParse(text, out id);

        if (answer)
        {
            var task = _taskService.GetById(id);
            if (task is null)
            {
                Console.WriteLine("Task not found,choose correct id");
                goto Id;
            }
            _taskService.Delete(task);
            Console.WriteLine("Task successfully deleted");
        }
        else
        {
            Console.WriteLine("Id must be a number");
            goto Id;
        }
    }
    public void ListAllTasks()
    {
        var tasks = _taskService.GetAll();
        if (tasks is null)
        {
            return;
        }
        foreach (var task in tasks)
        {
            Console.WriteLine($"ID:{task.Id} \n Description:{task.Description} \n Status:{task.Status} \n CreatedAt:{task.CreatedAt}");
        }
    }
    public void ListAllTasksByStatus()
    {
        Console.WriteLine("Choose status of tasks you want to list:\n1:Compeleted\n2:Todo\n3:In-Progress");
      Status:  string status = Console.ReadLine();
        int number;
        bool answer= int.TryParse(status, out number);
        if (answer)
        {
            switch (number)
            {
                case 1:
                    var completedTasks = _taskService.GetAllCompleted();
                    if (completedTasks is null)
                    {
                        Console.WriteLine("No tasks found");
                        goto Status;
                    }
                    foreach (var task in completedTasks)
                    {
                        Console.WriteLine($"ID:{task.Id} \n Description:{task.Description} \n Status:{task.Status} \n");
                    }
                    break;
                case 2:
                    var todoTasks = _taskService.GetAllNotDone();
                    if (todoTasks is null)
                    {
                        Console.WriteLine("No tasks found");
                        goto Status;
                    }
                    foreach (var task in todoTasks)
                    {
                        Console.WriteLine($"ID:{task.Id} \n Description:{task.Description} \n Status:{task.Status} \n");
                    }
                    break;
                case 3:
                    var inProgressTasks = _taskService.GetAllInProgress();
                    if (inProgressTasks is null)
                    {
                        Console.WriteLine("No tasks found");
                        goto Status;
                    }
                    foreach (var task in inProgressTasks)
                    {
                        Console.WriteLine($"ID:{task.Id} \n Description:{task.Description} \n Status:{task.Status}");
                    }
                    break;
                default:
                    Console.WriteLine("Please enter a valid status");
                    goto Status;
            }
        }
        else
        {
            Console.WriteLine("Please enter a valid status");
            goto Status;
        }
    }
    public void Mark()
    {
        GlobalErrorHandler.Handle(() =>
        {
            Console.WriteLine("Choose task to change mark:");
            Id : string text = Console.ReadLine();
            int id;
            if (int.TryParse(text, out id))
            {
                var task = _taskService.GetById(id);
                if (task is not null)
                {
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
                    _taskService.Mark(id, task);
                    Console.WriteLine("Task status successfully marked");
                    return;
                }
                goto Id;
            }
            else
            {
                Console.WriteLine("Please enter a valid id");
                goto Id;
            }
        });
        
    }
}