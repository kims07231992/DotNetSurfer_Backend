CREATE TABLE [dbo].[BK_Topics]
(
	[TopicId] INT NULL,
	[Title] NVARCHAR(20) NULL,
	[UserId] INT NULL,
	[Description] NVarChar(200) Null,
	[Picture] VARBINARY(MAX) NULL,
	[PictureMimeType] VARCHAR(50) NULL,
	[PictureUrl] NVARCHAR(2083) NULL,
	[PostDate] DATETIME2 NULL,
	[ModifyDate] DATETIME2 NULL,
	[ShowFlag] BIT NULL
);
GO