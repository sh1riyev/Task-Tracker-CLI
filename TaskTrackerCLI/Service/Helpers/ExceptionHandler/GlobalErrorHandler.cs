using TaskTrackerCLI.Service.Helpers.Extensions;

namespace TaskTrackerCLI.Service.Helpers.ExceptionHandler;

public static class GlobalErrorHandler
{
    public static void Handle(Action action)
    {
        try
        {
            action.Invoke();
        }
        catch (ArgumentNullException ex)
        {
            LogError("Null argument provided", ex);
        }
        catch (ArgumentException ex)
        {
            LogError("Invalid argument provided", ex);
        }
        catch (InvalidOperationException ex)
        {
            LogError("Invalid operation provided", ex);
        }
        catch (Exception ex)
        {
            LogError("An unexpected error occurred", ex);
        }
    }

    public static void LogError(string message, Exception ex)
    {
        ConsoleColor.Red.WriteConsole($"{message}\nError Detail: {ex.Message}");
    }
}