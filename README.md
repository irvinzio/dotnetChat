# Project Title

Real time chat with dotnet/Rsignal and angular

## Getting Started

These instructions will get you a copy of the project up and running on your local machine for development and testing purposes. See deployment for notes on how to deploy the project on a live system.

### Prerequisites

What things you need to install the software and how to install them

```
netcore 5x
https://dotnet.microsoft.com/download/dotnet/5.0

nodejs
https://nodejs.org/en/download/

mssql
https://www.microsoft.com/en-us/sql-server/sql-server-downloads
```

### Installing

1)vclone the repo 

2)change the connection string to your local database on appsetting.json file 

3)go to the root project folder and run the following command

```
dotnet run
```

this will dun de project create database and populate default test users

```
testpassword: TestTest123!
user: test1@test.com
user: test2@test.com
```

to access swagger documentation
```
https://localhost:5001/docs/index.html
```

to load login page
```
https://localhost:5001/login
```

## Running the tests

To do

## License

This project is licensed under the MIT License - see the [LICENSE.md](LICENSE.md) file for details