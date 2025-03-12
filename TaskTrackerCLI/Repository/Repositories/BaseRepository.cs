using TaskTrackerCLI.Models;
using TaskTrackerCLI.Data;
using TaskTrackerCLI.Repository.Repositories.Interfaces;

namespace TaskTrackerCLI.Repository.Repositories;

public class BaseRepository<T> :IBaseRepository<T> where T : BaseEntity
{
    public void Create(T model)
    {
        Data.DataContext<T>.Data.Add(model);
    }

    public void Update(T model,int id)
    {
        var entity = DataContext<T>.Data.FirstOrDefault(m => m.Id == id);
        entity = model;
    }

    public void Delete(T model)
    {
       Data.DataContext<T>.Data.Remove(model);
    }

    public List<T> GetAll()
    {
        return Data.DataContext<T>.Data.ToList();
    }

    public T GetById(int id)
    {
        return Data.DataContext<T>.Data.FirstOrDefault(m=>m.Id == id);
    }

    public List<T> GetAllWithExpression(Func<T, bool> expression)
    {
        return Data.DataContext<T>.Data.Where(expression).ToList();
    }
}