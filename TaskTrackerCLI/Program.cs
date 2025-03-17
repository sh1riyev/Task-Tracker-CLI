using TaskTrackerCLI.Controllers;
using TaskTrackerCLI.Service.Helpers.Enums;
using TaskTrackerCLI.Service.Helpers.Extensions;

TaskController taskController = new TaskController();

static void Headline()
{
    ConsoleColor.DarkYellow.WriteConsole("""

                                         1.Create Task
                                         2.Update Task
                                         3.Delete Task
                                         4.List All Tasks
                                         5.List Tasks by Status
                                         6.Mark Task
                                         """);
}

 void Main()
{
    var operations = new Dictionary<OperationType, Action>
    {
        { OperationType.TaskCreate, () => taskController.Create() },
        { OperationType.TaskUpdate, () => taskController.Update() },
        { OperationType.TaskDelete, () => taskController.Delete() },
        { OperationType.TaskListAll ,()=>taskController.ListAllTasks()},
        { OperationType.TaskListByStatus ,()=>taskController.ListAllTasksByStatus()},
        { OperationType.TaskMark ,()=>taskController.Mark()}
    };

    while (true)
    {
        Headline();
        string operationFormat=Console.ReadLine();
        if (int.TryParse(operationFormat, out int operation)&&Enum.IsDefined(typeof(OperationType), operation))
        {
            var operationType = (OperationType)operation;
            
            if (operations.ContainsKey(operationType))
            {
                operations[operationType].Invoke();
            }
            else
            {
                ConsoleColor.Red.WriteConsole("Operation not defined.");
            }
        }
        else
        {
            ConsoleColor.Red.WriteConsole("Invalid operation. Please choose a valid operation.");
        }
    }
}

Main();