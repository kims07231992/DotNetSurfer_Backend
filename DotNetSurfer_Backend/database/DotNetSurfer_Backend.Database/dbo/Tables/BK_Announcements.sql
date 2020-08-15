CREATE TABLE [dbo].[BK_Announcements]
(
	[AnnouncementId] INT NULL,
	[UserId] INT NULL,
	[StatusId] INT NULL,
	[Content] NVARCHAR(100) NULL,
	[PostDate] DATETIME2 NULL,
	[ModifyDate] DATETIME2 NULL,
	[ShowFlag] BIT NULL,
);
GO