# Golem

## Introduction

Golem is web application with advanced analytics of user actions. Data is collected from a web page to search for technology solutions, projects, or parts of them.
The software is developed in C #, TypeScript, and python programming languages.

Web application is available via https://golem.gq/

## Technologies

Target frameworks: netcoreapp3.1, Angular 9, Flask
Database : PostgreSQL

Tools&Technologies:
- Elasticsearch;
- GloVE;
- Docker;
- SendGrid;
- ipstack;
- .Net core identity;
- EF Core code first;
- AutoMapper;
- FluentValidation;

## Launch
Note: Geolocation data is not displayed during local startup because the system does not receive an IP address.

You can run the whole application using docker with command(inside src folder):
```bash
docker-compose up --build -d
```

You can also run only the API from the IDE (Rider, Microsoft Visual Studio) however, you must add DefaultConnection string for PostgreSQL and Elastisearch in the appsettings.

To run client side you need to open folder Golem-Client in Visual Studio Code or other environment. 
First install all packages, execute command in terminal:
```bash
npm install
```

To start client application execute command in terminal:
```bash
ng serve --o
```