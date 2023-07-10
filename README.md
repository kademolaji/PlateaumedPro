# PlateaumedPro API
PlateaumedPro Assessment Project

## Design Decisions
My understanding of this assessment is that I am task to create a CRUD opereation for Teacher and Student.
Not enough time to write lots of test
Not enough time to auto-generate student and teacher number
Added AuditTrail to track user activities

## Project Structure
This project is structured in two folder on solution level. The first folder is `src` and it contains `PlateaumedPro.Common`, `PlateaumedPro.Contracts`, `PlateaumedPro.Domain`, `PlateaumedPro.Services`, `PlateaumedPro.WebAPI`
> `PlateaumedPro.WebAPI` references `PlateaumedPro.Common`, `PlateaumedPro.Contracts`, `PlateaumedPro.Domain`, `PlateaumedPro.Services`

```.
├── src
│   ├── PlateaumedPro.Sln
│      ├── PlateaumedPro.Common
│      ├── PlateaumedPro.Contracts
│      ├── PlateaumedPro.Domain
│      ├── PlateaumedPro.Services
│      └── PlateaumedPro.WebAPI
│    
├── tests
        ├── PlateaumedPro.Tests
    
  

 ```
## Database
PostgreSQL was used for the database and the following table were included 
`Users`
`Strudents`
`AuditTrails`
`Teachers`
`CustomIdentities`
 
## Logging
 
NLog was used for logging operations. A generic LoggerService is created in `PlateaumedPro.Common` so that its available for all other project to log it operations. It's gathering all logs from Microsoft logging extensions and store it in specific log file. You can also configure file path by using NLog.config file.

## Security

Basic authentication was implemented that required the client the add Authorization header to the request. It works like bearer token, but in this case, `ApiKey` from the Users table is checked against the key gotten from the header. If `ApiKey` is not added to the request, you get Status Code 400 and if `ApiKey` does not match, you get Status Code 401. The Attribute is added on each Controller that needed to be authorized
Sample security token for test plateaumed_test_51JUu1MGNU5r3gGaDKd4aZKlcgy0IWF1px7EjZQlnNwfC9IRMy2uPQj3c0ZLhCLhyoHdhSFUXgewCXCN2nJeRWpro00W1qWBesM`

## To run this project
Create a PostgreSQL database 
Add connectionstring to appsetting.json file
Run update-database command
Build and run the project
## Exception Handling
A global exception handler middleware was written to handle all error and was used in the request pipeline.

## Testing
xUnit was used for the unit testing and every services was tested

