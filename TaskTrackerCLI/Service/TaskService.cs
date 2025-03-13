using System.Linq.Expressions;
using TaskTrackerCLI.Repository.Repositories;
using TaskTrackerCLI.Service.Interface;
using Task = TaskTrackerCLI.Models.Task;

namespace TaskTrackerCLI.Service;

public class TaskService : ITaskService
{
    private readonly TaskRepository _repository;
    private int count = 1;

    public TaskService()
    { 
        _repository = new TaskRepository();
    }

    public void Create(Task model)
    {
        if(model == null) throw new ArgumentNullException();
        model.Id = count;
        model.CreatedAt = DateTime.Now;
        _repository.Create(model);
        count++;
    }

    public void Update(int? id, Task model)
    {
     if (id == null) throw new ArgumentNullException();
     _repository.Update(model,(int)id);
    }

    public void Delete(int? id)
    {
        if (id == null) throw new ArgumentNullException();
        var model = _repository.GetById(Convert.ToInt32(id));
        if (model == null) throw new ArgumentNullException();
        _repository.Delete(model);
    }

    public void Mark(int? id, string process)
    {
        if(id == null) throw new ArgumentNullException();
        var model = _repository.GetById(Convert.ToInt32(id));
        if (model == null) throw new ArgumentNullException();
        model.Status = process;
    }

    public List<Task> GetAll()
    {
        var tasks =_repository.GetAll();
        if(!tasks.Any()) throw new ApplicationException("No tasks found");
        return tasks;
    }

    public List<Task> GetAllInProgress()
    {
        var tasks =_repository.GetAll();
        if(!tasks.Any(m=>m.Status=="In-Progress")) throw new ApplicationException("Currently there are no tasks in progress");
        return tasks;
    }

    public List<Task> GetAllCompleted()
    {
        var tasks =_repository.GetAll();
        if(!tasks.Any(m=>m.Status=="Completed")) throw new ApplicationException("Currently there are no completed tasks");
        return tasks;
    }

    public List<Task> GetAllNotDone()
    {
        var tasks =_repository.GetAll();
        if (!tasks.Any(m => m.Status == "todo"))
            throw new ApplicationException("Currently there are no tasks on the line ");
        return tasks;
    }
}