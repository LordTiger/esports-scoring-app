# ScoreCraft

This project was generated with [Angular CLI](https://github.com/angular/angular-cli) version 17.0.8.

## Tech Stack

### Frontend
- Angular v17 with Material Design and Ionic Components
  - [Angular CLI](https://angular.io/cli)
  
### Backend
- .NET 8 Web API
- Entity Framework

### Database
- SQL Server Express
  - [SQL Server Express Download](https://www.microsoft.com/en-za/sql-server/sql-server-downloads)
  
### Other
- Node.js
  - [Node.js Download](https://nodejs.org/en/download/current)
  - Make sure you have SQL Server Express installed.

## Prerequisites

Before running the project, ensure the following:

1. In Visual Studio, open the solution in the `scorecraftApi` directory.
2. Open the Package Manager Console and run the following commands:```Add-Migration [Migration Name] ```, ```Update-Database```

This will create the database if it does not exist.

3. In your preferred IDE (e.g., VS Code), navigate to the `scorecraft` directory in the terminal and run:
```npm i```


## How to Run the Project

### Backend (API)
1. Start the API using Visual Studio.

### Frontend (Angular App)
1. In the terminal, navigate to the `scorecraft` directory.
2. Run the following command:
3. ```npm start``` This script, defined in `package.json`, installs dependencies (`npm i`) and starts the Angular app (`ng serve -o`).
3. The application will automatically open in your web browser.

## How to Use the Project

Upon launching the application, you'll land on the Home section. From there, you can navigate to the following pages:

### Users Page
- Add, edit, or delete users.
- Users can be part of multiple teams.
- If no teams are present, the teams' selection will be disabled.
- If a team is marked as "Archived," it won't be selectable.

### Teams Page
- Add, view team details, edit, or soft delete (archive) a team.
- When team deletion takes place the team will be archived for historical data.

### Matches Page
- Add, view match details, edit, or soft delete (archive) a match.
- Match formats range from 1v1 to 5v5 based on the number of players assigned to each team.
- When viewing match details, you can:
  - Add results (only one chance, assuming results are final).
  - See match information such as best of, date, teams, and scores.
  - Review match history for each match played.

Note: Soft deletes (archiving) are utilized for teams and matches to preserve historical data.



