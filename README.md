# Flashcards Web Application

A modern web application built by Team Silicon for the Fundamentals of Software Engineering course at Seattle University. This project transforms the basic Contoso Crafts template into an interactive flashcards learning platform, demonstrating practical application of software engineering principles.

[Watch Demo Video](./Presentation-Team01.mp4)

## Technologies Used

- ASP.NET Core
- C#
- Razor Pages
- Server-Side Blazor
- Blazored.LocalStorage for client-side storage
- Bootstrap
- JSON for data storage
- xUnit for testing

## Project Structure

- `src/` - Main application source code
  - `Components/` - Razor components
  - `Controllers/` - API controllers
  - `Models/` - Data models
  - `Pages/` - Razor pages
  - `Services/` - Business logic and services
  - `wwwroot/` - Static files (CSS, JS, images)
- `UnitTests/` - Test project containing unit tests

## Getting Started

### Prerequisites

- .NET SDK (latest version)
- Visual Studio 2019+ or VS Code

### Running the Application

1. Clone the repository
2. Navigate to the project directory
3. Run the following commands:

```bash
cd src
dotnet restore
dotnet run
```

The application will start and be available at `https://localhost:5001` or `http://localhost:5000`

### Running Tests

```bash
cd UnitTests
dotnet test
```

## Features

- Create and manage flashcard categories
- Create, edit, and delete flashcards
- Interactive study mode
- Local storage for persistent data
- Responsive design
- REST API endpoints
- Comprehensive unit test coverage

## Contributing

1. Fork the repository
2. Create a feature branch
3. Commit your changes
4. Push to the branch
5. Create a Pull Request

## License

This project is licensed under the MIT License.
