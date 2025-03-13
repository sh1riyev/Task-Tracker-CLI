namespace TaskTrackerCLI.Repository.Data;

public static class DataContext<T>
{
    public static List<T> Data { get; set; }

    static DataContext()
    {
            Data = new List<T>();
    }
}