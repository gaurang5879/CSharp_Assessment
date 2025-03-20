# C# Assessment Projects

This repository contains five C# projects **CustomerApp.API**, **CustomerApp.Data**, **FactorialApp**, **WeatherApp**, and **TestProject**.

---

## **System Requirements**
Before running the projects, ensure your system has the following:

- **.NET 8 SDK** (Download from [Microsoft](https://dotnet.microsoft.com/en-us/download))
- **SQLite** (if using SQLite as the database)
  - Windows: [Download Precompiled Binaries](https://www.sqlite.org/download.html) and add to `PATH`

---

## **Project 1: CustomerApp.API**

### **Description**
A RESTful API built with ASP.NET Core that provides CRUD operations for `Customer` entities using **Entity Framework Core** and **SQLite**.

### **How to Run**
1. Open a terminal or command prompt.
2. Navigate to the project directory:
   ```sh
   cd CustomerApp.API
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Run the application:
   ```sh
   dotnet run
   ```
5. The API will start at `http://localhost:5000` or `https://localhost:5001`.

### **How to Change API Key**
- Open `appsettings.json`.
- Locate the `ApiSettings` section and update the `ApiKey` value.

---

## **Project 2: CustomerApp.Data**

### **Description**
A data access layer for `CustomerApp.API`, handling database interactions with **Entity Framework Core** and **SQLite**.

### **How to Run**
1. Open a terminal or command prompt.
2. Navigate to the project directory:
   ```sh
   cd CustomerApp.Data
   ```
3. Restore dependencies:
   ```sh
   dotnet restore
   ```
4. Apply database migrations:
   ```sh
   dotnet ef database update
   ```
5. This will create the required tables in `customers.db`.

---

## **Project 3: FactorialApp**

### **Description**
A C# console application that calculates the factorial of different numbers concurrently using **Task Parallel Library (TPL)** for better efficiency. It ensures **thread safety** using synchronization techniques and provides real-time execution tracking.

### **How to Run**
1. Open a terminal or command prompt.
2. Navigate to the project directory:
   ```sh
   cd FactorialApp
   ```
3. Run the application:
   ```sh
   dotnet run
   ```
4. Enter numbers when prompted, or type `exit` to quit.

### **Assumptions**
- The application will **execute calculations asynchronously**.
- Users can **cancel calculations mid-way** if needed.

---

## **Project 4: WeatherApp**

### **Description**
A C# console application that fetches the **current weather** of a given city using the **Weatherstack API**.

### **How to Run**
1. Open a terminal or command prompt.
2. Navigate to the project directory:
   ```sh
   cd WeatherApp
   ```
3. Open `appsettings.json` and replace `your_api_key` with a valid API key from [Weatherstack](https://weatherstack.com/dashboard).
4. Run the application:
   ```sh
   dotnet run
   ```
5. Enter the city name and country name when prompted, and the application will fetch and display the weather information.

### **How to Change API Key**
- Open `appsettings.json`.
- Locate the `WeatherApiSettings` section and update the `ApiKey` value.

### **Assumptions**
- The API key must be obtained **manually** from Weatherstack and **inserted in the configuration file**.
- The application fetches weather data **only for cities supported by Weatherstack**.

---

## **Running Unit Tests**
This repository contains **NUnit tests** for the CustomerApp and FactorialApp.

### **How to Run Tests**
1. Open a terminal or command prompt.
2. Navigate to the test project:
   ```sh
   cd Tests
   ```
3. Run the tests:
   ```sh
   dotnet test
   ```

---

## **Project Structure**
```
CSharp_Assessment/
│-- CustomerApp.API/      # RESTful API for customer management
│-- CustomerApp.Data/     # Data access layer for CustomerApp.API
│-- FactorialApp/         # Multi-threaded factorial calculator
│-- TestProject/          # NUnit test cases for Customer application
│-- WeatherApp/           # REST API consumer for weather data
│-- README.md             # Documentation
│-- .gitignore            # Git ignore file
```

---
