# Daily Currency Update Service

This project is designed to update currency data daily from an external API and store it in a local database. It consists of ASP.NET Core MVC application with background service for scheduled currency updates.

## Features

- Fetches currency data from an external API daily.
- Parses XML data and stores it in a local database.
- Provides endpoints to manually trigger currency update and delete all data.
- Scheduled background service updates currency data every 1 minute.

## Installation

1. Clone the repository to your local machine:

```bash
git clone https://github.com/FeryazHacimuradov/DailyCurrency.git
```

2. Navigate to the project directory:

```bash
cd DailyCurrency
```

3. Build the project:

```bash
dotnet build
```

4. Run the project:

```bash
dotnet run
```

5. Open your web browser and navigate to `https://localhost:port` to access the application.

## Usage

- Access the home page to view the currency data.

## Technologies Used

- ASP.NET Core MVC
- Entity Framework Core
- HttpClient for API requests
- NCrontab for scheduling background tasks

## Credits

- Developed by [Faryaz Hajimuradov]
