namespace TaskTrackerCLI.Service.Helpers.Extensions;

public static class ConsoleExtension
{
    public static void WriteConsole(this ConsoleColor foreground, string message)
    {
        Console.ForegroundColor = foreground;
        Console.WriteLine(message);
        Console.ResetColor();
    }
}