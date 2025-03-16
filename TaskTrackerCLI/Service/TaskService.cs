using System.Linq.Expressions;
using TaskTrackerCLI.Repository.Repositories;
using TaskTrackerCLI.Repository.Repositories.Interfaces;
using TaskTrackerCLI.Service.Helpers.ExceptionHandler;
using TaskTrackerCLI.Service.Interface;
using Task = TaskTrackerCLI.Models.Task;

namespace TaskTrackerCLI.Service;

public class TaskService : ITaskService
{
    private readonly ITaskRepository _repository;
    private int _count = 1;

    public TaskService()
    { 
        _repository = new TaskRepository();
    }

    public void Create(Task model)
    {
        GlobalErrorHandler.Handle(() =>
        {
            if(model == null) throw new ArgumentNullException(nameof(model), "Model cannot be null");
            model.Id = _count;
            model.CreatedAt = DateTime.Now;
            model.Status="In-Progress";
            _repository.Create(model);
            _count++;
        });
    }

    public void Update(int? id, Task model)
    {
        GlobalErrorHandler.Handle(() =>
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "Id cannot be null");
            model.UpdatedAt = DateTime.Now;
            _repository.Update(model, (int)id);

        });
    }

    public void Delete(int? id)
    {
        GlobalErrorHandler.Handle(() =>
        {
            if (!id.HasValue) 
                throw new ArgumentNullException(nameof(id), "ID cannot be null.");

            var model = _repository.GetById(id.Value);
            if (model == null) 
                throw new InvalidOperationException("Entity not found.");

            _repository.Delete(model);
        });
    }

    public void Mark(int? id, string process)
    {
        GlobalErrorHandler.Handle(()=>
        {
            if(id == null) throw new ArgumentNullException(nameof(id), "Id cannot be null");
            var model = _repository.GetById(Convert.ToInt32(id));
            if (model == null) throw new ArgumentNullException(nameof(model), "Entity not found.");
            model.Status = process;
        });
        
    }

    public Task? GetById(int? id)
    {
        return GlobalErrorHandler.Handle(() =>
        {
            if (id == null) throw new ArgumentNullException(nameof(id), "ID cannot be null.");
            var task = _repository.GetById((int)id);
            if (task == null) throw new ArgumentNullException("No tasks with this Id found");
            return task;
        });
    }

    public List<Task>? GetAll()
    {
        return GlobalErrorHandler.Handle(() =>
        {
            var tasks = _repository.GetAll();
            if (tasks.Count == 0) throw new ApplicationException("No tasks found");
            return tasks;
        });
    }

    public List<Task>? GetAllInProgress()
    {
        return GlobalErrorHandler.Handle(() =>
        {
            var tasks = _repository.GetAll();
            if (!tasks.Any(m => m.Status == "In-Progress"))
                throw new ApplicationException("Currently there are no tasks in progress");
            return tasks;
        });
    }

    public List<Task>? GetAllCompleted()
    {
        return GlobalErrorHandler.Handle(() =>
        {
            var tasks = _repository.GetAll();
            if (!tasks.Any(m => m.Status == "Completed"))
                throw new ApplicationException("Currently there are no completed tasks");
            return tasks;
        });
    }

    public List<Task>? GetAllNotDone()
    {
        return GlobalErrorHandler.Handle(() =>
        {
            var tasks = _repository.GetAll();
            if (!tasks.Any(m => m.Status == "todo"))
                throw new ApplicationException("Currently there are no tasks on the line ");
            return tasks;
        });
    }
}