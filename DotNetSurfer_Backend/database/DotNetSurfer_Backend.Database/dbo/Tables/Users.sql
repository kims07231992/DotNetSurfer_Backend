﻿CREATE TABLE [dbo].[Users]
(
	[UserId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[PermissionId] INT FOREIGN KEY REFERENCES [dbo].[Permissions](PermissionId) NOT NULL,
	[Name] NVARCHAR(20) NOT NULL,
	[Password] NVARCHAR(255) NOT NULL,
	[Email] NVARCHAR(320) NOT NULL,
	[Title] NVARCHAR(20) NULL,
	[Phone] NVARCHAR(20) NULL,
	[Address] NVARCHAR(100) NULL,
	[Introduction] NVARCHAR(100) NULL,
	[Birthdate] DATETIME2 NULL,
	[Picture] VARBINARY(MAX) NULL,
	[PictureMimeType] VARCHAR(50) NULL,
	[PictureUrl] NVARCHAR(2083) NULL,
)
GO