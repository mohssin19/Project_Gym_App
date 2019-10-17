use master
go

drop database if exists GymAppDB
go

create database GymAppDB
go

use GymAppDB

create table Users(
UserId NVARCHAR(3) NOT NULL PRIMARY KEY,
FirstName nvarchar(25) null,
LastName nvarchar(25) null,
[Address] nvarchar(60) null,
TelephoneNumber char(11) null,
Email nvarchar(40) null,
)

create table HealthRecord(
HealthRecordId INT NOT NULL IDENTITY PRIMARY KEY,
UserId NVARCHAR(3) null Foreign key references Users(UserId),
Age int null,
[Weight] decimal(3,2) null,
Height decimal(3,2) null,
BMR decimal(10,2) null,
BMI decimal(10,2) null,
KCAL decimal(10,2) null,
TargetBMI decimal(10,2) null
)


