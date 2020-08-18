--If database doesnt exist it is automatically created.
IF DB_ID('Zadatak_1') IS NULL
CREATE DATABASE Zadatak_1
GO
--Newly created database is set to be in use.
USE Zadatak_1
--All tables are reseted clean.
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblEmploye')
drop table tblEmploye
if exists (SELECT name FROM sys.sysobjects WHERE name = 'tblManger')
drop table tblManger

create table tblManger
(
ManagerID int primary key IDENTITY(1,1),
Firstname varchar(100),
Lastname varchar(100),
DateOfBirth Date,
Mail varchar(100),
Username varchar(100),
Password varchar(100),
Floor int,
Experience int,
EducationLevel varchar(100)
)

create table tblEmploye
(
EmployeID int primary key IDENTITY(1,1),
Firstname varchar(100),
Lastname varchar(100),
DateOfBirth Date,
Mail varchar(100),
Username varchar(100),
Password varchar(100),
Floor int,
Gender varchar(100),
Citizenship varchar(100),
Duty varchar(100),
Salary decimal,
ManagerID int foreign key references tblManger(ManagerID) not null
)