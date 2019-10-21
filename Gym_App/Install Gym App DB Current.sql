use master
go

drop database if exists GymAppDB
go

create database GymAppDB
go

use GymAppDB


CREATE TABLE [dbo].[Users] (
    [UserId]          INT           IDENTITY (1000, 1) NOT NULL,
    [FirstName]       NVARCHAR (25) NULL,
    [LastName]        NVARCHAR (25) NULL,
    [Address]         NVARCHAR (60) NULL,
    [TelephoneNumber] CHAR (11)     NULL,
    [Email]           NVARCHAR (40) NULL,
    PRIMARY KEY CLUSTERED ([UserId] ASC)
	);
	
	CREATE TABLE [dbo].[HealthRecord] (
    [HealthRecordId] INT             IDENTITY (1, 1) NOT NULL,
    [UserId]         INT             NULL,
    [Age]            INT             NULL,
    [Weight]         DECIMAL (10, 2) NULL,
    [Height]         DECIMAL (10, 2) NULL,
    [BMR]            DECIMAL (10, 2) NULL,
    [BMI]            DECIMAL (10, 2) NULL,
    [KCAL]           DECIMAL (10, 2) NULL,
    [TargetBMI]      DECIMAL (10, 2) NULL,
    PRIMARY KEY CLUSTERED ([HealthRecordId] ASC),
    FOREIGN KEY ([UserId]) REFERENCES [dbo].[Users] ([UserId])
);
create table HealthRecord(
HealthRecordId INT NOT NULL IDENTITY PRIMARY KEY,
UserId INT null Foreign key references Users(UserId),
Age int null,
[Weight] decimal(3,2) null,
Height decimal(3,2) null,
BMR decimal(10,2) null,
BMI decimal(10,2) null,
KCAL decimal(10,2) null,
TargetBMI decimal(10,2) null
)

CREATE TABLE [dbo].[Admin](
	[StaffID] [int] IDENTITY(1,1) NOT NULL,
	[Username] [varchar](50) NULL,
	[Password] [varchar](50) NULL,
	[IsAdmin] [bit] NOT NULL,
 CONSTRAINT [PK_Admin] PRIMARY KEY CLUSTERED 
(
	[StaffID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO


