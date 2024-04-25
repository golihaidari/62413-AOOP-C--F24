# OnlineStore Project

## Introduction

Welcome to the OnlineStore project! This project delves into the development process of an application designed for an online wholesale company, as part of the Advanced Object-Oriented Programming (AOOP) course utilizing C# .NET. OnlineStore serves as a comprehensive solution for wholesale operations, providing a centralized platform for purchasing various products.

## Project Overview

OnlineStore aims to develop a robust software solution for an online wholesale company. By utilizing C# and .NET, we seek to build an interactive application that streamlines the purchasing process for both retailers and customers. With a focus on advanced object-oriented programming principles, we aim to demonstrate our proficiency in leveraging the capabilities of the .NET framework and the C# language.

Currently, the solution is designed for customers, with plans to implement an admin interface in the future.

OnlineStore's deployment encompasses three versions hosted in separate folders on GitHub:

1. **Desktop App**: Utilizes a WPF desktop application.
2. **Web API**: Employs a REST web API.
3. **Web API Test**: Employs NUnit testing on the Web API.

All three versions share a common origin, ensuring comprehensive documentation of features and choices relevant to each iteration.

## Functionality

OnlineStore provides a range of features to enhance the user experience:

- Customers can register, log in/out, browse product listings, manage their shopping baskets, view costs, and complete purchases with ease.
- The Web API architecture enables future enhancements and the development of customer-driven applications, allowing for flexibility and scalability.

## Deployment

OnlineStore's deployment encompasses three versions hosted in separate folders on GitHub:

1. **Desktop App**: Utilizes a WPF desktop application.
2. **Web API**: Employs a REST web API.
3. **Web API Test**: Employs NUnit testing on the Web API.

All three versions share a common origin, ensuring comprehensive documentation of features and choices relevant to each iteration.

## Installation Instructions

To set up and run the OnlineStore project, follow these steps:

1. **Install and Configure SQL Express Local**: Download and install SQL Express Local, which is free. Configure it as necessary for your environment.

2. **Set Default Connection String**: In the Web API project, navigate to the app settings file and set the default connection string to point to your SQL Express Local instance.

3. **Apply Migrations**: From the Visual Studio Package Manager Console, use the following commands:
   ```sh
   add-migration InitialCreate
   update-database

This will initialize the database with the necessary schema.

To try the app, create a password with the following requirements:
  - 12 characters in length
  - Includes at least one uppercase letter, one lowercase letter, one special character (such as #), and one number.
