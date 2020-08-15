CREATE TABLE [dbo].[BK_Articles]
(
	[ArticleId] INT NULL,
	[TopicId] INT NULL,
	[UserId] INT NULL,
	[Title] NVARCHAR(100) NULL,
	[Content] NVARCHAR(MAX) NULL,
	[Category] NVARCHAR(10) NULL,
	[Picture] VARBINARY(MAX) NULL,
	[PictureMimeType] VARCHAR(50) NULL,
	[PictureUrl] NVARCHAR(2083) NULL,
	[PostDate] DATETIME2 NULL,
	[ModifyDate] DATETIME2 NULL,
	[ReadCount] INT NULL,
	[ShowFlag] BIT NULL
);
GO