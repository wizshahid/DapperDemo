CREATE DATABASE DapperDemoDB

USE DapperDemoDB

CREATE TABLE Employee
(
Id uniqueidentifier primary key not null,
Name Nvarchar(100) NOT NULL,
Address Nvarchar(500),
Salary INT,
Age INT,
Gender Nvarchar(10) NOT NULL
)