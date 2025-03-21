using System.Linq.Expressions;
using Task = TaskTrackerCLI.Models.Task;
namespace TaskTrackerCLI.Service.Interface;

public interface ITaskService
{
    void Create(Task? model);
    void Update(int?id,Task? model);
    void Delete(Task? model);
    void Mark(int?id,Task? model);
    Task? GetById(int ? id);
    List<Task>?  GetAll();
    List<Task>? GetAllInProgress();
    List<Task>? GetAllCompleted();
    List<Task>? GetAllNotDone();
}