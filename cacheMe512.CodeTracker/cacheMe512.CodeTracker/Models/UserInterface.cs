using Spectre.Console;

using static cacheMe512.CodeTracker.Models.Enums;


namespace cacheMe512.CodeTracker.Models;

internal class UserInterface
{
    private readonly SessionsController _sessionsController = new();

    internal void MainMenu()
    {
        while (true)
        {
            Console.Clear();

            var actionChoice = AnsiConsole.Prompt(
                new SelectionPrompt<MenuAction>()
                .Title("What do you want to do next?")
                .AddChoices(Enum.GetValues<MenuAction>()));

            switch (actionChoice)
            {
                case MenuAction.ViewSessions:
                    ViewSessions(_sessionsController);
                    break;
                case MenuAction.AddSession:
                    AddSession(_sessionsController);
                    break;
                case MenuAction.DeleteSession:
                    //_sessionsController.DeleteSession();
                    break;
            }

        }
    }

    private static void ViewSessions(SessionsController sessionsController)
    {
        var table = new Table();
        table.AddColumn("[yellow]ID[/]");
        table.AddColumn("[yellow]Start Time[/]");
        table.AddColumn("[yellow]End Time[/]");
        table.AddColumn("[yellow]Duration[/]");

        foreach (var session in sessionsController.GetAllSessions())
        {
            table.AddRow(
                session.Id.ToString(),
                $"[cyan]{session.StartTime}[/]",
                $"[cyan]{session.EndTime}[/]",
                $"[cyan]{session.Duration}[/]"
            );
        }

        AnsiConsole.Write(table);
        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }

    private static void AddSession(SessionsController sessionsController)
    {
        DateTime startTime = Validation.GetDateTimeInput("Enter the session start time using format (dd-MM-yy HH:mm):");
        DateTime endTime = Validation.GetDateTimeInput("Enter the session end time using format (dd-MM-yy HH:mm):");
        
        var newSession = new CodingSession
        {
            StartTime = startTime,
            EndTime = endTime
        };

        sessionsController.InsertSession(newSession);

        AnsiConsole.MarkupLine("[green]Session added successfully![/]");
    }
}
