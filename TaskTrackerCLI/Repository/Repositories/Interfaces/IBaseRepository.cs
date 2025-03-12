using TaskTrackerCLI.Models;

namespace TaskTrackerCLI.Repository.Repositories.Interfaces;

public interface IBaseRepository<T> where T : BaseEntity
{
    void Create(T model);
    void Update(T model,int id);
    void Delete(T model);
    List<T> GetAll();
    T GetById(int id);
    List<T> GetAllWithExpression(Func<T, bool> expression);
}