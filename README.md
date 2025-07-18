# 🎭 Theatre Ticket Office

This project implements an information system for managing a theatre playbill and distributing tickets. The system allows users to search for performances, reserve tickets, and process ticket sales.

## 📌 Features

- Search for performances by author, title, genre, or date  
- Ticket management: selling and reserving tickets  
- Multiple ticket prices per event

## 🛠️ Technologies Used

- **.NET 9**
- **ASP.NET Core**
- **Entity Framework Core 9.0**
- **SQL Server**
- **xUnit** (for testing)
- **AutoMapper**
- **Moq**, **FluentAssertions** for unit testing
  
## 🚀 Getting Started

1. Clone the repository:
   ```bash
   git clone https://github.com/olha-makarchuk/BoxTicketApi.git
   cd BoxTicketApi
   
2. Install dependencies and build the project:
    ```bash
    dotnet restore
    dotnet build

3. Apply EF Core migrations:
   ```bash
   dotnet ef database update --project BoxTicketApi.DAL

4. Run the API:
   ```bash
   dotnet run --project BoxTicketApi

📦 Dependencies

See dependencies.txt for a full list of NuGet packages and versions per project.

 - You can regenerate this file anytime using:
   ```bash
   dotnet list package > dependencies.txt
   
🧪 Testing
 - Run all tests using:
    ```bash
    dotnet test


