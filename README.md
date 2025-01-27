# Coding Session Tracker

A simple command-line application to track coding sessions. Users can record the start and end times of their coding sessions, view session details, delete sessions, and update session information.

## Features

- **Add Session**: Record a new coding session with start and end times.
- **View Sessions**: View a list of all recorded coding sessions.
- **Start New Session**: Start tracking a new session in real time.
- **End Session**: Stop the stopwatch and save the session's duration.
- **Update Session**: Modify the start and end times of an existing session.
- **Delete Session**: Remove a session from the database.

## Requirements

- .NET 6.0 or later
- SQLite (Database is automatically created)

## Installation

1. **Clone the repository**
2. **Install dependencies**
3. **Run the program**

This will start the application and show the main menu in the terminal.

## Usage

When you run the program, you will be presented with the **Main Menu** with the following options:

1. **View Sessions**: View a list of all recorded coding sessions with their start times, end times, and durations.
2. **Add Session**: Manually input a start time and end time for a new coding session.
3. **Start New Session**: Start a new session that will track your time using a stopwatch.
4. **End Session**: Stop the stopwatch, calculate the session's duration, and save it to the database.
5. **Update Session**: Select an existing session and modify its start time and end time.
6. **Delete Session**: Remove a session from the database.
7. **Exit**: Exit the application.
