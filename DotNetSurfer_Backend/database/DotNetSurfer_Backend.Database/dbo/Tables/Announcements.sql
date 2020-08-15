CREATE TABLE [dbo].[Announcements]
(
	[AnnouncementId] INT IDENTITY(1, 1) NOT NULL PRIMARY KEY,
	[UserId] INT FOREIGN KEY REFERENCES [dbo].[Users](UserId) NULL,
	[StatusId] INT FOREIGN KEY REFERENCES [dbo].[Statuses](StatusId) NULL,
	[Content] NVARCHAR(100) NULL,
	[PostDate] DATETIME2 DEFAULT GETDATE() NULL,
	[ModifyDate] DATETIME2 DEFAULT GETDATE() NULL,
	[ShowFlag] BIT NOT NULL DEFAULT 1
);
GO

--Backup
CREATE TRIGGER trg_BackupAnnouncements ON [dbo].[Announcements]
AFTER DELETE
AS
BEGIN
	INSERT INTO [dbo].[BK_Announcements] 
	SELECT * 
	FROM DELETED
END
GO