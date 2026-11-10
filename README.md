# C# Lab 1 - Windows Forms Application

## Project Overview

This project is a Windows Forms desktop application developed using C#. It combines a login system with two interactive programs: a Bicycle Rental Calculator and a Mushroom Guesser.

The application demonstrates fundamental C# programming concepts, event handling, conditional statements, loops, input validation, and Windows Forms GUI development.

## Features

### 1. Login System
- Username and password authentication.
- Password masking.
- Login validation.
- Error messages for invalid credentials.

**Default Login Credentials:**
- Username: `admin`
- Password: `1234`

### 2. Program Selection

After successful login, users can choose between:
- Bicycle Rental
- Mushroom Guesser

### 3. Bicycle Rental Calculator

Calculates the total bicycle rental cost based on the selected starting and ending hours.

**Hourly Pricing:**

| Time | Rate per Hour |
|---|---:|
| 00:00 - 07:00 | 500 RWF |
| 07:00 - 14:00 | 1,000 RWF |
| 14:00 - 19:00 | 1,500 RWF |
| 19:00 - 21:00 | 1,000 RWF |
| 21:00 - 24:00 | 500 RWF |

Features:
- Accepts whole-number inputs.
- Validates starting and ending hours.
- Calculates charges for each rental hour separately.
- Displays the total cost in RWF.

### 4. Mushroom Guesser

An interactive guessing program that identifies one of six mushrooms by asking up to three Yes/No questions.

**Supported Mushrooms:**
- Agaric jaunissant
- Amanite tue-mouche
- Cepe de Bordeaux
- Coprin chevelu
- Girolle
- Pied bleu

## Technologies Used

- C#
- .NET
- Windows Forms
- Visual Studio

## How to Run the Project

1. Clone this repository:

   ```bash
   git clone YOUR_REPOSITORY_URL
   ```

2. Open the project folder.
3. Open the `.sln` solution file in Visual Studio.
4. Build the solution.
5. Run the application using `F5` or the Start button.

## Concepts Demonstrated

- Object-Oriented Programming fundamentals
- Classes and inheritance
- Windows Forms GUI development
- Event handling
- If-else statements
- Loops (`for`)
- Methods and parameters
- Input validation using `int.TryParse()`
- Message boxes
- Form navigation

## Author

**Ruterana Gloire**

C# Windows Forms Lab 1 Project
