# Introduce Warehouse Project [![Build Status](https://travis-ci.org/muratoner/Demo.svg?branch=master)](https://travis-ci.org/muratoner/Demo)
Manage company products with warehouse project. You can add, delete, update, search products.

Product grid created with pagination and grid data seed with server side.

If you want search product then you can search with any column on grid search bar.

Automatic created database and seed product table with 100.000 records when you will first project runing.

Showing product count in dashboard page.

## Used Technologies & Principles
- Angular 8: Using for SPA.
- .Net Core: Using for Web API and UI Layers.
- Entity Framework Core: Using for database operations.
- SQLite: Using for data store.
- Repository
- Layered Structure
- Swagger: You can using swagger for web api methods test on created swagger user interface.
- Web API: Using for our main SPA project, other mobile devices and other authorized web projects. 

## My Contact
- E-Mail: info@muratoner.net
- Github: https://github.com/muratoner
- LinkedIn: https://linkedin.com/in/muratoner
- My Blog: http://muratoner.net
- Stackoverflow: https://stackoverflow.com/users/6289085

## Installation

1. Download project from github.
2. Install NodeJS.(Recomend install last version) - https://nodejs.org/en/
3. Install Git - https://git-scm.com/
3. Install Angular CLI on npm package manager. - https://cli.angular.io/
4. Install .Net Core
5. Run `npm -i` command in `Warehouse.Web>ClientApp` folder.
6. Run `dotnet run` command in `Warehouse.Web` project folder. It will running on 50050 Port.
6. Run `dotnet run` command in `Warehouse.Api` project folder. It will running on 54048 Port.

## Problems & Solution
- When you run `ng build` command in `Warehouse.Web>ClientApp` folder or when you run `dotnet run` command in `Warehouse.Web` project folder if you see as `Error: Can't find Python executable "python", you can set the PYTHON env variable.` error then run `npm --add-python-to-path='true' --debug install --global windows-build-tools` command.
