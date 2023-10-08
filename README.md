# MinimalApis for use with any DB (used .NET Core 7)

Communicate with your DB via WEB API throught Minimal Web Api and Basic Authentication (you can change it , vay of authenticate and authorize..)

how to use this web api examples and meanings :

/* cancel authentication */\
https://localhost:7202/api/pro/Get\
TEST - hello world \

/* show methods on swagger */\
https://localhost:7202/swagger/\

Examples of use with Northwind (if supposed you did connection string to Northwind db) \

/* select * from Customers */
https://localhost:7202/api/pro/Table/dbo.Customers

/* select EmployeeId,FirstName,LastName,BirthDate from Employees */
https://localhost:7202/api/pro/TableF/dbo.Employees/EmployeeId,FirstName,LastName,BirthDate


for other methodes you can check either Methods either Swagger to learn use and expand it ...

Valon Hoti
2023.10.08
Prishtine 
Republic Of Kosova
valon.hoti@gmail.com



