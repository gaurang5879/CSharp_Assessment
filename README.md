# C# Assessment Projects

This repository contains three C# projects **multi-threading**, **database operations using Entity Framework**, and **consuming REST APIs**.

---

## **Project 1: Multi-threaded Factorial Calculation**

### **Description**
This is a C# console application that calculates the factorial of different numbers concurrently using multiple threads. It ensures **thread safety** using synchronization techniques.

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

### **Assumptions**
- The numbers for which factorial is calculated are **hardcoded** inside the application.
- The application will **execute all threads concurrently** and print results in any order.

---

## **Project 2: Customer Management using Entity Framework**

### **Description**
A C# console application that implements **CRUD operations** (Create, Read, Update, Delete) for a `Customer` entity using **Entity Framework** and **SQLite**.

### **How to Run**
1. Open a terminal or command prompt.
2. Navigate to the project directory:
   ```sh
   cd CustomerApp
   ```
3. Run the application:
   ```sh
   dotnet run
   ```
4. This will:
   - Create a local SQLite database (`customers.db`).
   - Add a sample customer.
   - Fetch and display customers.
   - Update and delete a customer as part of the example flow.

### **Assumptions**
- **SQLite** is used by default (`Data Source=customers.db`).
- The database is **automatically created** on first run.

---

## **Project 3: Weather API Consumer**

### **Description**
A C# console application that fetches the **current weather** of a given city using the **Weatherstack API**.

### **How to Run**
1. Open a terminal or command prompt.
2. Navigate to the project directory:
   ```sh
   cd WeatherApp
   ```
3. Open `Program.cs` and replace `your_api_key` with a valid API key from [Weatherstack](https://weatherstack.com/dashboard).
4. Run the application:
   ```sh
   dotnet run
   ```
5. Enter the city name when prompted, and the application will fetch and display the weather information.

### **Assumptions**
- The API key must be obtained **manually** from Weatherstack and **inserted in the code**.
- The application fetches weather data **only for cities supported by Weatherstack**.

---

## **Running Unit Tests**
This repository contains **NUnit tests** for the Customer Management application.

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

### **Implemented Test Cases**
- **`AddCustomer_ShouldIncreaseCount()`** → Tests if adding a customer increases the count.
- **`DeleteCustomer_ShouldDecreaseCount()`** → Tests if deleting a customer decreases the count.

---

## **Project Structure**
```
CSharp_Assessment/
│-- FactorialApp/     # Multi-threaded factorial calculator
│-- CustomerApp/          # CRUD application using Entity Framework
│-- WeatherApp/           # REST API consumer for weather data
│-- TestProject/                # NUnit test cases
│-- README.md             # Documentation
│-- .gitignore            # Git ignore file
```

---
