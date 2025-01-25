using Spectre.Console;

using static cacheMe512.CodeTracker.Models.Enums;
using cacheMe512.CodeTracker.Models;


namespace cacheMe512.CodeTracker;

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
                .Title("MAIN MENU")
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
                    DeleteSession(_sessionsController);
                    break;
                case MenuAction.Exit:
                    return;
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
        DateTime startTime;
        DateTime endTime;

        while (true)
        {
            startTime = Validation.GetDateTimeInput("Enter the session start time using format (dd-MM-yy HH:mm):");
            endTime = Validation.GetDateTimeInput("Enter the session end time using format (dd-MM-yy HH:mm):");

            if (Validation.DateTimeInSequence(startTime, endTime))
            {
                break;
            }
            else
            {
                Validation.DisplayMessage("End date must be later than Start date\n", "red");
            }
        }

        var newSession = new CodingSession
        {
            StartTime = startTime,
            EndTime = endTime
        };

        sessionsController.InsertSession(newSession);

        Validation.DisplayMessage("Session added successfully!", "green");
        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }

    private static void DeleteSession(SessionsController sessionsController)
    {
        var sessionsToDelete = sessionsController.GetAllSessions();
        if(!sessionsToDelete.Any())
        {
            Validation.DisplayMessage("No coding sessions available to delete.[/]", "red");
            Console.ReadKey();
            return;
        }

        var sessionToDelete = AnsiConsole.Prompt(
            new SelectionPrompt<CodingSession>()
                .Title("Select a coding session to [red]delete[/]:")
                .UseConverter(s => $"Start: {s.StartTime} End: {s.EndTime} Duration: {s.Duration}")
                .AddChoices(sessionsToDelete));

        if(Validation.ConfirmDeletion(sessionToDelete))
        {
            if(sessionsController.DeleteSession(sessionToDelete.Id))
            {
                Validation.DisplayMessage("Session deleted successfully!");
            }
            else
            {
                Validation.DisplayMessage("Session not found.", "red");
            }
        }
        else
        {
            Validation.DisplayMessage("Deletion canceled.");
        }

        AnsiConsole.MarkupLine("Press Any Key to Continue.");
        Console.ReadKey();
    }
}
