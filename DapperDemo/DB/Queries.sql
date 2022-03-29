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

--Date 28/03/2022
CREATE TABLE Account
(
	Id uniqueidentifier primary key not null,
	Username nvarchar(100) not null,
	Password nvarchar(100) not null
)
