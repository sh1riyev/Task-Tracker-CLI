using TaskTrackerCLI.Repository.Repositories.Interfaces;
using Task = TaskTrackerCLI.Models.Task;

namespace TaskTrackerCLI.Repository.Repositories;

public class TaskRepository : BaseRepository<Task>, ITaskRepository
{
    
}